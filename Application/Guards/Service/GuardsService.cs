using Application.Account.Service.Interfaces;
using Application.AccountManagement.Dtos.Authentication;
using Application.Guards.Dtos;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Application.Guards.Service
{
    internal class GuardsService(IUnitOfWorkService ufw, IMapper mapper, UserManager<ApplicationUser> userManager, IRegistrationService registrationService) : IGuardsService
    {
        public async Task<Result<Empty>> CreateAsync(RegisterGuardRequest registerRequest, CancellationToken cancellationToken = default)
        {
            var validationResult = await registrationService.ValidateRegistrationAsync(registerRequest.Phone, registerRequest.Email);
            if (validationResult != null) return validationResult;

            var guard = mapper.Map<Guard>(registerRequest);
            guard.EmailConfirmed = true;
            guard.PhoneNumberConfirmed = true;

            var registrationResults = await userManager.CreateAsync(guard, registerRequest.Password);
            if (!registrationResults.Succeeded)
                return new ValidationError(registrationResults.Errors.Select(er => er.Description));

            await userManager.AddClaimsAsync(guard, [new Claim(CustomClaims.UserId, guard.Id.ToString()), new Claim(CustomClaims.FirstName, guard.FirstName)]);

            return new()
            {
                Status = StatusCodes.Status200OK,
                IsSuccess = true,
                SuccessCode = SuccessCodes.UserRegistered
            };

        }

        public async Task<Result<Empty>> UpdateAsync(UpdateGuardDto dto)
        {
            var guard = await userManager.Users
                    .OfType<Guard>()
                    .Include(g => g.Address)
                    .FirstOrDefaultAsync(u => u.Id == dto.Id);

            if (guard is null)
                return new NotFoundError();

            if (await userManager.Users.AnyAsync(u => u.PhoneNumber == dto.Phone && u.Id != dto.Id))
                return new ConflictError(ErrorCodes.PhoneExists);

            if (await userManager.Users.AnyAsync(u => u.Email == dto.Email && u.Id != dto.Id))
                return new ConflictError(ErrorCodes.EmailExists);

            if (await userManager.Users.AnyAsync(u => u.UserName == dto.UserName && u.Id != dto.Id))
                return new ConflictError(ErrorCodes.UserNameExists);

            mapper.Map(dto, guard);

            await ufw.SaveChangesAsync();

            return Result<Empty>.Success(Empty.Default, SuccessCodes.GuardUpdated);

        }

        public async Task<Result<GuardDto>> GetByIdAsync(long id)
        {
            var guard = await userManager.Users
                    .OfType<Guard>()
                    .Include(g => g.Address)
                    .FirstOrDefaultAsync(u => u.Id == id);

            if (guard == null)
                return new NotFoundError();

            var guardDto = mapper.Map<GuardDto>(guard);
            return Result<GuardDto>.Success(guardDto, SuccessCodes.GuardReceived);

        }

        public async Task<Result<GuardDto[]>> GetAllAsync()
        {
            var guards = await userManager.Users
                    .OfType<Guard>()
                    .Include(g => g.Address)
                    .Include(g => g.Skills)
                    .Include(g => g.PrevCompanies)
                    .ToListAsync();

            var guardsDto = mapper.Map<GuardDto[]>(guards);
            return Result<GuardDto[]>.Success(guardsDto, SuccessCodes.GuardsReceived);

        }


        public async Task<Result<Empty>> DeleteAsync(long id)
        {
            var guard = await userManager.Users
                                  .OfType<Guard>()
                                  .Include(g => g.Address)
                                    .Include(g => g.Skills)
                                   .Include(g => g.PrevCompanies)
                                  .FirstOrDefaultAsync(u => u.Id == id);
            if (guard == null)
                return new NotFoundError();


            await userManager.DeleteAsync(guard);

            return Result<Empty>.Success(Empty.Default, SuccessCodes.GuardDeleted);
        }
    }
}
