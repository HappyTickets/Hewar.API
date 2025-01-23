using Application.Account.Service.Interfaces;
using Application.Facilities.Dtos;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Facilities.Service
{
    internal class FacilitiesService(IUnitOfWorkService ufw, IMapper mapper, UserManager<ApplicationUser> userManager,
        IRegistrationService registrationService
            ) : IFacilitiesService
    {
        public async Task<Result<Empty>> CreateAsync(CreateFacilityDto dto)
        {
            var validationResult = await registrationService.ValidateRegistrationAsync(dto.Phone, dto.Email, Roles.Facility);
            if (validationResult != null) return validationResult;
            var user = registrationService.CreateUserBase(dto.Email, dto.Phone, AccountTypes.Facility, dto.ImageUrl, true);

            user.Facility = new Facility
            {
                Name = dto.Name,
                Type = dto.Type,
                CommercialRegistration = dto.CommercialRegistration,
                ActivityType = dto.ActivityType,
                Address = dto.Address,
                City = dto.City,
                ResponsibleName = dto.ResponsibleName,
                ResponsiblePhone = dto.ResponsiblePhone,
            };
            var res = await registrationService.RegisterAccountAsync(user, dto.Password, Roles.Facility);
            return res.IsSuccess ? Result<Empty>.Success(Empty.Default, SuccessCodes.FacilityCreated) : res;

        }

        public async Task<Result<Empty>> UpdateAsync(UpdateFacilityDto dto)
        {
            var facility = await ufw.Facilities.GetByIdAsync(dto.Id, ["LoginDetails"]);

            if (facility == null)
                return new NotFoundError();

            if (await userManager.Users.AnyAsync(u => u.PhoneNumber == dto.Phone && u.Facility.Id != dto.Id))
                return new ConflictError(ErrorCodes.PhoneExists);

            if (await userManager.Users.AnyAsync(u => u.Email == dto.Email && u.Facility.Id != dto.Id))
                return new ConflictError(ErrorCodes.EmailExists);


            facility.LoginDetails.Email = dto.Email;
            facility.LoginDetails.PhoneNumber = dto.Phone;
            facility.LoginDetails.ImageUrl = dto.ImageUrl;
            facility.Type = dto.Type;
            facility.Name = dto.Name;
            facility.CommercialRegistration = dto.CommercialRegistration;
            facility.ActivityType = dto.ActivityType;
            facility.Address = dto.Address;
            facility.City = dto.City;
            facility.ResponsibleName = dto.ResponsibleName;
            facility.ResponsiblePhone = dto.ResponsiblePhone;

            await ufw.SaveChangesAsync();

            return Result<Empty>.Success(Empty.Default, SuccessCodes.FacilityUpdated);

        }

        public async Task<Result<FacilityDto>> GetByIdAsync(long id)
        {
            var facility = await ufw.Facilities.GetByIdAsync(id, [nameof(Facility.LoginDetails)]);

            if (facility == null)
                return new NotFoundError();

            var facilityDto = mapper.Map<FacilityDto>(facility);
            return Result<FacilityDto>.Success(facilityDto, SuccessCodes.FacilityReceived);

        }

        public async Task<Result<FacilityDto[]>> GetAllAsync()
        {
            var facilities = await ufw.Facilities.GetAllAsync([nameof(Facility.LoginDetails)]);

            var facilitiesDto = mapper.Map<FacilityDto[]>(facilities);
            return Result<FacilityDto[]>.Success(facilitiesDto, SuccessCodes.FacilitiesReceived);
        }

        public async Task<Result<Empty>> SoftDeleteAsync(long id)
        {
            var facility = await ufw.Facilities.GetByIdAsync(id, [nameof(Facility.LoginDetails)]);

            if (facility == null)
                return new NotFoundError();

            ufw.Facilities.SoftDelete(facility);
            await ufw.SaveChangesAsync();

            return Result<Empty>.Success(Empty.Default, SuccessCodes.FacilitySoftDeleted);

        }

        public async Task<Result<Empty>> HardDeleteAsync(long id)
        {
            var facility = await ufw.Facilities.GetByIdAsync(id, [nameof(Facility.LoginDetails)]);

            if (facility == null)
                return new NotFoundError();

            using (var tran = await ufw.BeginTransactionAsync())
            {
                try
                {
                    ufw.Facilities.HardDelete(facility);
                    await userManager.DeleteAsync(facility.LoginDetails);
                    await ufw.SaveChangesAsync();

                    await tran.CommitAsync();
                }
                catch
                {
                    await tran.RollbackAsync();
                }
            }

            return Result<Empty>.Success(Empty.Default, SuccessCodes.FacilityHardDeleted);

        }
    }
}
