using Application.Account.Service.Interfaces;
using Domain.Entities.CompanyAggregate;
using Domain.Entities.FacilityAggregate;
using Domain.Events.Accounts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Application.Account.Service.Concrete
{
    public class RegistrationService(
        UserManager<ApplicationUser> userManager,
        RoleManager<ApplicationRole> roleManager,
        IUnitOfWorkService ufw,
        IPublisher publisher) : IRegistrationService
    {

        public async Task<Result<Empty>> ValidateRegistrationAsync(string phone, string email)
        {
            if (await IsPhoneNumberTaken(phone))
                return new ConflictError(ErrorCodes.PhoneExists);

            if (await userManager.Users.AnyAsync(u => u.Email == email))
                return new ConflictError(ErrorCodes.EmailExists);

            return null;
        }


        public async Task<Tuple<long, EntityTypes>> CreateFacilityAsync(Facility facility)
        {
            await ufw.GetRepository<Facility>().CreateAsync(facility);
            await ufw.SaveChangesAsync();
            return new Tuple<long, EntityTypes>(facility.Id, EntityTypes.Facility);
        }

        public async Task<Tuple<long, EntityTypes>> CreateCompanyAsync(Company company)
        {
            await ufw.GetRepository<Company>().CreateAsync(company);
            await ufw.SaveChangesAsync();
            return new Tuple<long, EntityTypes>(company.Id, EntityTypes.Company);
        }

        public async Task<Result<Empty>> RegisterEntityWithAdminAsync(
          ApplicationUser adminUser,
          string password,
          string roleName,
          Func<Task<Tuple<long, EntityTypes>>> entityCreationFunction,
          CancellationToken cancellationToken)
        {
            #region Validation
            var validationResult = await ValidateRegistrationAsync(adminUser.PhoneNumber, adminUser.Email);
            if (validationResult != null) return validationResult;
            #endregion

            #region Role Initialization
            var role = new ApplicationRole
            {
                Name = roleName,
                NormalizedName = roleName.ToUpper(),
            };
            #endregion

            using var transaction = await ufw.BeginTransactionAsync();
            try
            {
                #region Entity Creation
                var (entityId, entityType) = await entityCreationFunction();
                #endregion

                #region Role Creation
                var roleResult = await roleManager.CreateAsync(role);
                if (!roleResult.Succeeded)
                {
                    await transaction.RollbackAsync(cancellationToken);
                    return new ConflictError(ErrorCodes.RoleCreationFailed);
                }
                #endregion

                #region User Creation & Role Assignment
                var userResult = await userManager.CreateAsync(adminUser, password);
                if (!userResult.Succeeded)
                {
                    await transaction.RollbackAsync(cancellationToken);
                    return new ConflictError(ErrorCodes.AssignUserRoleFailed);
                }

                await userManager.AddToRoleAsync(adminUser, roleName);
                await userManager.AddClaimsAsync(adminUser,
                    [new Claim(CustomClaims.EntityId, entityId.ToString()), new Claim(CustomClaims.EntityType, entityType.ToString())]);

                #endregion

                #region Commit Transaction and Publish Event
                await transaction.CommitAsync(cancellationToken);
                if (!adminUser.EmailConfirmed)
                    await publisher.Publish(new AccountCreated(adminUser));

                return new Result<Empty> { Status = StatusCodes.Status200OK, IsSuccess = true, SuccessCode = SuccessCodes.Created };
                #endregion
            }
            catch (Exception ex)
            {
                #region Rollback and Error Handling
                await transaction.RollbackAsync(cancellationToken);
                // Log exception if necessary
                return new ConflictError(ErrorCodes.Conflict);
                #endregion
            }
        }




        private async Task<bool> IsPhoneNumberTaken(string phoneNumber)
            => await userManager.Users.AnyAsync(u => u.PhoneNumber == phoneNumber);
    }

}

