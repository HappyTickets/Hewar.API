using Application.Companies.Dtos;
using AutoMapper;
using Domain.Entities.UserEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Application.Companies.Service
{
    internal class CompaniesService: ICompaniesService
    {
        private readonly IUnitOfWorkService _ufw;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public CompaniesService(IUnitOfWorkService ufw, IMapper mapper, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            _ufw = ufw;
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<Result<Empty>> CreateAsync(CreateCompanyDto dto)
        {
            if (await _userManager.Users.AnyAsync(u => u.PhoneNumber == dto.Phone))
                return new ConflictException(Resource.PhoneNumber_Unique_Validation);

            if (await _userManager.Users.AnyAsync(u => u.Email == dto.Email))
                return new ConflictException(Resource.EmailExistsError);

            if (!await _roleManager.RoleExistsAsync(Roles.Company))
                return new NotFoundException(Resource.RoleNotExistError);

            var user = new ApplicationUser
            {
                UserName = null,
                Email = dto.Email,
                EmailConfirmed = true,
                PhoneNumber = dto.Phone,
                PhoneNumberConfirmed = true,
                AccountType = AccountTypes.Company,
                Company = new()
                {
                    Name = dto.Name,
                    ImageUrl = dto.ImageUrl,
                    Address = dto.Address
                }
            };

            var registrationResults = await _userManager.CreateAsync(user, dto.Password);

            if (!registrationResults.Succeeded)
                return new ValidationException(registrationResults.Errors.Select(er => er.Description));

            await _userManager.AddToRoleAsync(user, Roles.Company);
            await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.NameIdentifier, user.Company.Id.ToString()));

            return Empty.Default;
        }

        public async Task<Result<Empty>> UpdateAsync(UpdateCompanyDto dto)
        {
            var company = await _ufw.Companies.GetByIdAsync(dto.Id, ["LoginDetails"]);

            if (company == null)
                return new NotFoundException();

            if (await _userManager.Users.AnyAsync(u => u.PhoneNumber == dto.Phone && u.Company.Id != dto.Id))
                return new ConflictException(Resource.PhoneNumber_Unique_Validation);

            if (await _userManager.Users.AnyAsync(u => u.Email == dto.Email && u.Company.Id != dto.Id))
                return new ConflictException(Resource.EmailExistsError);

            company.Name = dto.Name;
            company.ImageUrl = dto.ImageUrl;
            company.Address = dto.Address;
            company.LoginDetails.Email = dto.Email;
            company.LoginDetails.PhoneNumber = dto.Phone;

            await _ufw.SaveChangesAsync();

            return Empty.Default;
        }

        public async Task<Result<CompanyDto>> GetByIdAsync(long id)
        {
            var company = await _ufw.Companies.GetByIdAsync(id, ["LoginDetails"]);

            if (company == null)
                return new NotFoundException();

            return _mapper.Map<CompanyDto>(company);
        }

        public async Task<Result<CompanyDto[]>> GetAllAsync()
        {
            var companies = await _ufw.Companies.GetAllAsync(["LoginDetails"]);

            return _mapper.Map<CompanyDto[]>(companies);
        }

        public async Task<Result<Empty>> SoftDeleteAsync(long id)
        {
            var company = await _ufw.Companies.GetByIdAsync(id, ["LoginDetails"]);

            if (company == null)
                return new NotFoundException();

            _ufw.Companies.SoftDelete(company);
            await _ufw.SaveChangesAsync();

            return Empty.Default;
        }

        public async Task<Result<Empty>> HardDeleteAsync(long id)
        {
            var company = await _ufw.Companies.GetByIdAsync(id, ["LoginDetails"]);

            if (company == null)
                return new NotFoundException();

            using(var tran = await _ufw.BeginTransactionAsync())
            {
                try
                {
                    _ufw.Companies.HardDelete(company);
                    await _userManager.DeleteAsync(company.LoginDetails);
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
