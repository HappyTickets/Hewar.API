using Application.Account.Service.Interfaces;
using Application.Companies.Dtos;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Companies.Service
{
    internal class CompaniesService(IUnitOfWorkService ufw, IMapper mapper, UserManager<ApplicationUser> userManager, IRegistrationService registrationService) : ICompaniesService
    {

        public async Task<Result<Empty>> CreateAsync(CreateCompanyDto dto)
        {
            var validationResult = await registrationService.ValidateRegistrationAsync(dto.Phone, dto.Email, Roles.Company);
            if (validationResult != null) return validationResult;

            var user = registrationService.CreateUserBase(dto.Email, dto.Phone, AccountTypes.Company, dto.ImageUrl, true);
            user.Company = new Company
            {
                Name = dto.Name,
                Address = dto.Address,
            };

            var res = await registrationService.RegisterAccountAsync(user, dto.Password, Roles.Company);

            return res.IsSuccess ? Result<Empty>.Success(Empty.Default, SuccessCodes.CompanyCreated) : res;

        }

        public async Task<Result<Empty>> UpdateAsync(UpdateCompanyDto dto)
        {
            var company = await ufw.Companies.GetByIdAsync(dto.Id, [nameof(Company.LoginDetails)]);

            if (company == null)
                return new NotFoundError();

            if (await userManager.Users.AnyAsync(u => u.PhoneNumber == dto.Phone && u.Company.Id != dto.Id))
                return new ConflictError(ErrorCodes.PhoneExists);

            if (await userManager.Users.AnyAsync(u => u.Email == dto.Email && u.Company.Id != dto.Id))
                return new ConflictError(ErrorCodes.EmailExists);

            company.Name = dto.Name;
            company.Address = dto.Address;
            company.LoginDetails.Email = dto.Email;
            company.LoginDetails.PhoneNumber = dto.Phone;
            company.LoginDetails.ImageUrl = dto.ImageUrl;

            await ufw.SaveChangesAsync();

            return Result<Empty>.Success(Empty.Default, SuccessCodes.CompanyUpdated);

        }

        public async Task<Result<CompanyDto>> GetByIdAsync(long id)
        {
            var company = await ufw.Companies.GetByIdAsync(id, [nameof(Company.LoginDetails)]);

            if (company == null)
                return new NotFoundError();

            var companyDto = mapper.Map<CompanyDto>(company);
            return Result<CompanyDto>.Success(companyDto, SuccessCodes.CompanyReceived);

        }

        public async Task<Result<CompanyDto[]>> GetAllAsync()
        {
            var companies = await ufw.Companies.GetAllAsync([nameof(Company.LoginDetails)]);

            var companiesDto = mapper.Map<CompanyDto[]>(companies);
            return Result<CompanyDto[]>.Success(companiesDto, SuccessCodes.CompaniesReceived);

        }

        public async Task<Result<Empty>> SoftDeleteAsync(long id)
        {
            var company = await ufw.Companies.GetByIdAsync(id, [nameof(Company.LoginDetails)]);

            if (company == null)
                return new NotFoundError();

            ufw.Companies.SoftDelete(company);
            await ufw.SaveChangesAsync();

            return Result<Empty>.Success(Empty.Default, SuccessCodes.CompanySoftDeleted);
            ;
        }

        public async Task<Result<Empty>> HardDeleteAsync(long id)
        {
            var company = await ufw.Companies.GetByIdAsync(id, [nameof(Company.LoginDetails)]);

            if (company == null)
                return new NotFoundError();

            using (var tran = await ufw.BeginTransactionAsync())
            {
                try
                {
                    ufw.Companies.HardDelete(company);
                    await userManager.DeleteAsync(company.LoginDetails);
                    await ufw.SaveChangesAsync();

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
