using Application.Account.Service.Interfaces;
using Domain.Entities.CompanyAggregate;
using Domain.Entities.FacilityAggregate;
using Domain.Events.Accounts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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


        public async Task<Result<Empty>> CreateFacilityAsync(Facility facility)
        {
            await ufw.Facilities.CreateAsync(facility);
            await ufw.SaveChangesAsync();
            return new()
            {
                Status = StatusCodes.Status200OK,
                IsSuccess = true,
                SuccessCode = SuccessCodes.FacilityCreated,
            };
        }

        public async Task<Result<Empty>> CreateCompanyAsync(Company company)
        {
            await ufw.Companies.CreateAsync(company);
            await ufw.SaveChangesAsync();
            return new()
            {
                Status = StatusCodes.Status200OK,
                IsSuccess = true,
                SuccessCode = SuccessCodes.CompanyCreated,
            };
        }

        public async Task<Result<Empty>> RegisterEntityWithAdminAsync(
        ApplicationUser adminUser,
        string password,
        string roleName,
        Func<Task<Result<Empty>>> entityCreationFunction,
        CancellationToken cancellationToken)
        {
            var validationResult = await ValidateRegistrationAsync(adminUser.PhoneNumber, adminUser.Email);
            if (validationResult != null) return validationResult;

            var role = new ApplicationRole
            {
                Name = roleName,
                NormalizedName = roleName.ToUpper(),
            };

            using var transaction = await ufw.BeginTransactionAsync();
            try
            {
                var roleResult = await roleManager.CreateAsync(role);
                if (!roleResult.Succeeded)
                {
                    await transaction.RollbackAsync(cancellationToken);
                    return new ConflictError(ErrorCodes.RoleCreationFailed);
                }

                var userResult = await userManager.CreateAsync(adminUser, password);
                if (!userResult.Succeeded)
                {
                    await transaction.RollbackAsync(cancellationToken);
                    return new ConflictError(ErrorCodes.AssignUserRoleFailed);
                }

                await userManager.AddToRoleAsync(adminUser, roleName);

                var entityResult = await entityCreationFunction();

                if (!entityResult.IsSuccess)
                {
                    await transaction.RollbackAsync(cancellationToken);
                    return entityResult;
                }

                await transaction.CommitAsync(cancellationToken);
                await publisher.Publish(new AccountCreated(adminUser));

                return entityResult;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(cancellationToken);
                // Log exception if necessary
                return new ConflictError(ErrorCodes.Conflict);
            }
        }




        private async Task<bool> IsPhoneNumberTaken(string phoneNumber)
            => await userManager.Users.AnyAsync(u => u.PhoneNumber == phoneNumber);
    }

}

