using Application.Companies.Dtos;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Application.Companies.Service
{
    internal class CompaniesService : ICompaniesService
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
                return new ConflictError(ErrorCodes.PhoneExists);

            if (await _userManager.Users.AnyAsync(u => u.Email == dto.Email))
                return new ConflictError(ErrorCodes.EmailExists);

            if (!await _roleManager.RoleExistsAsync(Roles.Company))
                return new NotFoundError(ErrorCodes.RoleNotExists);

            var user = new ApplicationUser
            {
                UserName = null,
                Email = dto.Email,
                EmailConfirmed = true,
                PhoneNumber = dto.Phone,
                PhoneNumberConfirmed = true,
                AccountType = AccountTypes.Company,
                ImageUrl = dto.ImageUrl,
                Company = new()
                {
                    Name = dto.Name,
                    Address = dto.Address
                }
            };

            var registrationResults = await _userManager.CreateAsync(user, dto.Password);

            if (!registrationResults.Succeeded)
                return new ValidationError(registrationResults.Errors.Select(er => er.Description));

            await _userManager.AddToRoleAsync(user, Roles.Company);
            await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.NameIdentifier, user.Company.Id.ToString()));

            return Result<Empty>.Success(Empty.Default, SuccessCodes.CompanyCreated);

        }

        public async Task<Result<Empty>> UpdateAsync(UpdateCompanyDto dto)
        {
            var company = await _ufw.Companies.GetByIdAsync(dto.Id, ["LoginDetails"]);

            if (company == null)
                return new NotFoundError();

            if (await _userManager.Users.AnyAsync(u => u.PhoneNumber == dto.Phone && u.Company.Id != dto.Id))
                return new ConflictError(ErrorCodes.PhoneExists);

            if (await _userManager.Users.AnyAsync(u => u.Email == dto.Email && u.Company.Id != dto.Id))
                return new ConflictError(ErrorCodes.EmailExists);

            company.Name = dto.Name;
            company.Address = dto.Address;
            company.LoginDetails.Email = dto.Email;
            company.LoginDetails.PhoneNumber = dto.Phone;
            company.LoginDetails.ImageUrl = dto.ImageUrl;

            await _ufw.SaveChangesAsync();

            return Result<Empty>.Success(Empty.Default, SuccessCodes.CompanyUpdated);

        }

        public async Task<Result<CompanyDto>> GetByIdAsync(long id)
        {
            var company = await _ufw.Companies.GetByIdAsync(id, ["LoginDetails"]);

            if (company == null)
                return new NotFoundError();

            var companyDto = _mapper.Map<CompanyDto>(company);
            return Result<CompanyDto>.Success(companyDto, SuccessCodes.CompanyReceived);

        }

        public async Task<Result<CompanyDto[]>> GetAllAsync()
        {
            var companies = await _ufw.Companies.GetAllAsync(["LoginDetails"]);

            var companiesDto = _mapper.Map<CompanyDto[]>(companies);
            return Result<CompanyDto[]>.Success(companiesDto, SuccessCodes.CompaniesReceived);

        }

        public async Task<Result<Empty>> SoftDeleteAsync(long id)
        {
            var company = await _ufw.Companies.GetByIdAsync(id, ["LoginDetails"]);

            if (company == null)
                return new NotFoundError();

            _ufw.Companies.SoftDelete(company);
            await _ufw.SaveChangesAsync();

            return Result<Empty>.Success(Empty.Default, SuccessCodes.CompanySoftDeleted);
            ;
        }

        public async Task<Result<Empty>> HardDeleteAsync(long id)
        {
            var company = await _ufw.Companies.GetByIdAsync(id, ["LoginDetails"]);

            if (company == null)
                return new NotFoundError();

            using (var tran = await _ufw.BeginTransactionAsync())
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

            return Result<Empty>.Success(Empty.Default, SuccessCodes.CompanyHardDeleted);

        }
    }
}
