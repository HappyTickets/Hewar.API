using Application.Companies.Dtos;
using Application.Facilities.Dtos;
using AutoMapper;
using Domain.Entities.UserEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Facilities.Service
{
    internal class FacilitiesService: IFacilitiesService
    {
        private readonly IUnitOfWorkService _ufw;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public FacilitiesService(IUnitOfWorkService ufw, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _ufw = ufw;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<Result<Empty>> UpdateAsync(UpdateFacilityDto dto)
        {
            var facility = await _ufw.Facilities.GetByIdAsync(dto.Id, ["LoginDetails"]);

            if (facility == null)
                return new NotFoundException();

            if (await _userManager.Users.AnyAsync(u => u.Email == dto.Email && u.Facility.Id != dto.Id))
                return new ConflictException(Resource.EmailExistsError);


            facility.LoginDetails.Email = dto.Email;
            facility.LoginDetails.PhoneNumber = dto.Phone;
            facility.Type = dto.Type;
            facility.Name = dto.Name;
            facility.CommercialRegistration = dto.CommercialRegistration;
            facility.ActivityType = dto.ActivityType;
            facility.Address = dto.Address;
            facility.City = dto.City;
            facility.ResponsibleName = dto.ResponsibleName;
            facility.ResponsiblePhone = dto.ResponsiblePhone;

            await _ufw.SaveChangesAsync();

            return Empty.Default;
        }

        public async Task<Result<FacilityDto>> GetByIdAsync(long id)
        {
            var facility = await _ufw.Facilities.GetByIdAsync(id, ["LoginDetails"]);

            if (facility == null)
                return new NotFoundException();

            return _mapper.Map<FacilityDto>(facility);
        }

        public async Task<Result<FacilityDto[]>> GetAllAsync()
        {
            var facilities = await _ufw.Facilities.GetAllAsync(["LoginDetails"]);

            return _mapper.Map<FacilityDto[]>(facilities);
        }

        public async Task<Result<Empty>> SoftDeleteAsync(long id)
        {
            var facility = await _ufw.Facilities.GetByIdAsync(id, ["LoginDetails"]);

            if (facility == null)
                return new NotFoundException();

            _ufw.Facilities.SoftDelete(facility);
            await _ufw.SaveChangesAsync();

            return Empty.Default;
        }

        public async Task<Result<Empty>> HardDeleteAsync(long id)
        {
            var facility = await _ufw.Facilities.GetByIdAsync(id, ["LoginDetails"]);

            if (facility == null)
                return new NotFoundException();

            using (var tran = await _ufw.BeginTransactionAsync())
            {
                try
                {
                    _ufw.Facilities.HardDelete(facility);
                    await _userManager.DeleteAsync(facility.LoginDetails);
                    await _ufw.SaveChangesAsync();

                    await tran.CommitAsync();
                }
                catch
                {
                    await tran.RollbackAsync();
                }
            }

            return Empty.Default;
        }
    }
}
