using Application.Companies.Dtos;
using Application.Facilities.Dtos;
using AutoMapper;
using Domain.Entities.UserEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Application.Facilities.Service
{
    internal class FacilitiesService: IFacilitiesService
    {
        private readonly IUnitOfWorkService _ufw;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public FacilitiesService(IUnitOfWorkService ufw, IMapper mapper, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            _ufw = ufw;
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<Result<Empty>> CreateAsync(CreateFacilityDto dto)
        {
            if (await _userManager.Users.AnyAsync(u => u.PhoneNumber == dto.Phone))
                return new ConflictException(Resource.PhoneNumber_Unique_Validation);

            if (await _userManager.Users.AnyAsync(u => u.Email == dto.Email))
                return new ConflictException(Resource.EmailExistsError);

            if (!await _roleManager.RoleExistsAsync(Roles.Facility))
                return new NotFoundException(Resource.RoleNotExistError);

            var user = new ApplicationUser
            {
                UserName = null,
                Email = dto.Email,
                EmailConfirmed = true,
                PhoneNumber = dto.Phone,
                PhoneNumberConfirmed = true,
                AccountType = AccountTypes.Facility,
                Facility = new()
                {
                    Name = dto.Name,
                    ImageUrl = dto.ImageUrl,
                    Type = dto.Type,
                    CommercialRegistration = dto.CommercialRegistration,
                    ActivityType = dto.ActivityType,
                    Address = dto.Address,
                    City = dto.City,
                    ResponsibleName = dto.ResponsibleName,
                    ResponsiblePhone = dto.ResponsiblePhone,
                }
            };

            var registrationResults = await _userManager.CreateAsync(user, dto.Password);

            if (!registrationResults.Succeeded)
                return new ValidationException(registrationResults.Errors.Select(er => er.Description));

            await _userManager.AddToRoleAsync(user, Roles.Facility);
            await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.NameIdentifier, user.Facility.Id.ToString()));

            return Empty.Default;
        }

        public async Task<Result<Empty>> UpdateAsync(UpdateFacilityDto dto)
        {
            var facility = await _ufw.Facilities.GetByIdAsync(dto.Id, ["LoginDetails"]);

            if (facility == null)
                return new NotFoundException();

            if (await _userManager.Users.AnyAsync(u => u.PhoneNumber == dto.Phone && u.Facility.Id != dto.Id))
                return new ConflictException(Resource.PhoneNumber_Unique_Validation);
            
            if (await _userManager.Users.AnyAsync(u => u.Email == dto.Email && u.Facility.Id != dto.Id))
                return new ConflictException(Resource.EmailExistsError);


            facility.LoginDetails.Email = dto.Email;
            facility.LoginDetails.PhoneNumber = dto.Phone;
            facility.Type = dto.Type;
            facility.Name = dto.Name;
            facility.ImageUrl = dto.ImageUrl;
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
