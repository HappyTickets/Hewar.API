using Application.Guards.Dtos;
using AutoMapper;
using Domain.Entities.UserEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Application.Guards.Service
{
    internal class GuardsService: IGuardsService
    {
        private readonly IUnitOfWorkService _ufw;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public GuardsService(IUnitOfWorkService ufw, IMapper mapper, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            _ufw = ufw;
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<Result<Empty>> CreateAsync(CreateGuardDto dto)
        {
            if (await _userManager.Users.AnyAsync(u => u.PhoneNumber == dto.Phone))
                return new ConflictException(Resource.PhoneNumber_Unique_Validation);

            if (await _userManager.Users.AnyAsync(u => u.Email == dto.Email))
                return new ConflictException(Resource.EmailExistsError);

            if (await _userManager.Users.AnyAsync(u => u.UserName == dto.UserName))
                return new ConflictException(Resource.UserNameExistsError);

            if (!await _roleManager.RoleExistsAsync(Roles.Guard))
                return new NotFoundException(Resource.RoleNotExistError);

            var user = new ApplicationUser
            {
                UserName = dto.UserName,
                Email = dto.Email,
                EmailConfirmed = true,
                PhoneNumber = dto.Phone,
                PhoneNumberConfirmed = true,
                AccountType = AccountTypes.Guard,
                Guard = new()
                {
                    FirstName = dto.FirstName,
                    LastName = dto.LastName,
                    ImageUrl = dto.ImageUrl,
                    DateOfBirth = dto.DateOfBirth,
                    NationalId = dto.NationalId,
                    Qualification = dto.Qualification,
                    City = dto.City,
                    BloodType = dto.BloodType,
                    Height = dto.Height,
                    Weight = dto.Weight,
                    Skills = _mapper.Map<ICollection<Skill>>(dto.Skills),
                    PrevCompanies = _mapper.Map<ICollection<PrevCompany>>(dto.PrevCompanies),
                }
            };

            var registrationResults = await _userManager.CreateAsync(user, dto.Password);

            if (!registrationResults.Succeeded)
                return new ValidationException(registrationResults.Errors.Select(er => er.Description));

            await _userManager.AddToRoleAsync(user, Roles.Guard);
            await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.NameIdentifier, user.Guard.Id.ToString()));

            return Empty.Default;


            return Empty.Default;
        }

        public async Task<Result<Empty>> UpdateAsync(UpdateGuardDto dto)
        {
            var guard = await _ufw.Guards.GetByIdAsync(dto.Id, ["LoginDetails"]);

            if (guard == null)
                return new NotFoundException();

            if (await _userManager.Users.AnyAsync(u => u.PhoneNumber == dto.Phone && u.Guard.Id != dto.Id))
                return new ConflictException(Resource.PhoneNumber_Unique_Validation);

            if (await _userManager.Users.AnyAsync(u => u.Email == dto.Email && u.Guard.Id != dto.Id))
                return new ConflictException(Resource.EmailExistsError);

            if (await _userManager.Users.AnyAsync(u => u.UserName == dto.UserName && u.Guard.Id != dto.Id))
                return new ConflictException(Resource.UserNameExistsError);

            guard.FirstName = dto.FirstName;
            guard.LastName = dto.LastName;
            guard.ImageUrl = dto.ImageUrl;
            guard.DateOfBirth = dto.DateOfBirth;
            guard.NationalId = dto.NationalId;
            guard.Qualification = dto.Qualification;
            guard.City = dto.City;
            guard.BloodType = dto.BloodType;
            guard.Height = dto.Height;
            guard.Weight = dto.Weight;
            guard.Skills = _mapper.Map<ICollection<Skill>>(dto.Skills);
            guard.PrevCompanies = _mapper.Map<ICollection<PrevCompany>>(dto.PrevCompanies);
            guard.LoginDetails.UserName = dto.UserName;
            guard.LoginDetails.Email = dto.Email;
            guard.LoginDetails.PhoneNumber = dto.Phone;

            await _ufw.SaveChangesAsync();

            return Empty.Default;
        }

        public async Task<Result<GuardDto>> GetByIdAsync(long id)
        {
            var guard = await _ufw.Guards.GetByIdAsync(id, ["LoginDetails"]);

            if (guard == null)
                return new NotFoundException();

            return _mapper.Map<GuardDto>(guard);
        }

        public async Task<Result<GuardDto[]>> GetAllAsync()
        {
            var guards = await _ufw.Guards.GetAllAsync(["LoginDetails"]);

            return _mapper.Map<GuardDto[]>(guards);
        }

        public async Task<Result<Empty>> SoftDeleteAsync(long id)
        {
            var guard = await _ufw.Guards.GetByIdAsync(id, ["LoginDetails"]);

            if (guard == null)
                return new NotFoundException();

            _ufw.Guards.SoftDelete(guard);
            await _ufw.SaveChangesAsync();

            return Empty.Default;
        }

        public async Task<Result<Empty>> HardDeleteAsync(long id)
        {
            var guard = await _ufw.Guards.GetByIdAsync(id, ["LoginDetails"]);

            if (guard == null)
                return new NotFoundException();

            using (var tran = await _ufw.BeginTransactionAsync())
            {
                try
                {
                    _ufw.Guards.HardDelete(guard);
                    await _userManager.DeleteAsync(guard.LoginDetails);
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
