using Application.Account.Service.Interfaces;
using Application.AccountManagement.Dtos.Authentication;
using Application.Facilities.Dtos;
using AutoMapper;
using Domain.Entities.FacilityAggregate;

namespace Application.Facilities.Service
{
    internal class FacilitiesService
        (IUnitOfWorkService ufw,
        IMapper mapper,
        IRegistrationService registrationService) : IFacilitiesService
    {


        public async Task<Result<Empty>> CreateAsync(RegisterFacilityRequest registerRequest, CancellationToken cancellationToken = default)
        {
            var adminUser = mapper.Map<ApplicationUser>(registerRequest.AdminInfo);
            adminUser.EmailConfirmed = true;
            adminUser.PhoneNumberConfirmed = true;

            var facility = mapper.Map<Facility>(registerRequest);
            var roleName = $"{facility.Name} Admin";

            return await registrationService.RegisterEntityWithAdminAsync(adminUser, registerRequest.AdminInfo.Password, roleName, () => registrationService.CreateFacilityAsync(facility), cancellationToken);
        }


        public async Task<Result<Empty>> UpdateAsync(UpdateFacilityDto dto)
        {
            var facility = await ufw.GetRepository<Facility>().GetByIdAsync(dto.Id, [nameof(Facility.Address)]);

            if (facility is null)
                return new NotFoundError();

            facility.Type = dto.Type;
            facility.Name = dto.Name;
            facility.CommercialRegistration = dto.CommercialRegistration;
            facility.ActivityType = dto.ActivityType;
            facility.Address = mapper.Map<Address>(dto.Address);
            facility.ResponsibleName = dto.ResponsibleName;
            facility.ResponsiblePhone = dto.ResponsiblePhone;
            facility.Logo = dto.Logo;

            await ufw.SaveChangesAsync();

            return Result<Empty>.Success(Empty.Default, SuccessCodes.FacilityUpdated);

        }

        public async Task<Result<FacilityDto>> GetByIdAsync(long id)
        {
            var facility = await ufw.GetRepository<Facility>().GetByIdAsync(id, [nameof(Facility.Address)]);

            if (facility is null) return new NotFoundError();

            var facilityDto = mapper.Map<FacilityDto>(facility);
            return Result<FacilityDto>.Success(facilityDto, SuccessCodes.FacilityReceived);

        }

        public async Task<Result<FacilityDto[]>> GetAllAsync()
        {
            var facilities = await ufw.GetRepository<Facility>().GetAllAsync([nameof(Facility.Address)]);

            var facilitiesDto = mapper.Map<FacilityDto[]>(facilities);
            return Result<FacilityDto[]>.Success(facilitiesDto, SuccessCodes.FacilitiesReceived);
        }

        public async Task<Result<Empty>> SoftDeleteAsync(long id)
        {
            var facility = await ufw.GetRepository<Facility>().GetByIdAsync(id, [nameof(Facility.Address)]);

            if (facility is null)
                return new NotFoundError();

            ufw.GetRepository<Facility>().SoftDelete(facility);
            await ufw.SaveChangesAsync();

            return Result<Empty>.Success(Empty.Default, SuccessCodes.FacilitySoftDeleted);

        }

        public async Task<Result<Empty>> HardDeleteAsync(long id)
        {
            var facility = await ufw.GetRepository<Facility>().GetByIdAsync(id, [nameof(Facility.Address)]);

            if (facility == null)
                return new NotFoundError();

            ufw.GetRepository<Facility>().HardDelete(facility);
            await ufw.SaveChangesAsync();

            return Result<Empty>.Success(Empty.Default, SuccessCodes.FacilityHardDeleted);

        }
    }
}
