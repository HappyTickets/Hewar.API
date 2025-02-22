using Application.Account.Service.Interfaces;
using Application.AccountManagement.Dtos.Authentication;
using Application.Companies.Dtos;
using AutoMapper;
using Domain.Entities.CompanyAggregate;

namespace Application.Companies.Service
{
    internal class CompaniesService(IUnitOfWorkService ufw, IMapper mapper, IRegistrationService registrationService) : ICompaniesService
    {
        public async Task<Result<Empty>> CreateAsync(RegisterCompanyRequest registerRequest, CancellationToken cancellationToken = default)
        {
            var adminUser = mapper.Map<ApplicationUser>(registerRequest.AdminInfo);
            adminUser.EmailConfirmed = true;
            adminUser.PhoneNumberConfirmed = true;

            var company = mapper.Map<Company>(registerRequest);
            var roleName = $"{company.Name} Admin";

            return await registrationService.RegisterEntityWithAdminAsync(adminUser, registerRequest.AdminInfo.Password, roleName, () => registrationService.CreateCompanyAsync(company), cancellationToken);
        }

        public async Task<Result<Empty>> UpdateAsync(UpdateCompanyDto dto)
        {
            var company = await ufw.GetRepository<Company>().GetByIdAsync(dto.Id, [nameof(Company.Address)]);

            if (company is null)
                return new NotFoundError();

            company.Name = dto.Name;
            company.PhoneNumber = dto.PhoneNumber;
            company.RegistrationNumber = dto.RegistrationNumber;
            company.ContactEmail = dto.ContactEmail;
            company.Address = mapper.Map<Address>(dto.Address);
            company.TaxId = dto.TaxId;
            company.Logo = dto.Logo;

            await ufw.SaveChangesAsync();

            return Result<Empty>.Success(Empty.Default, SuccessCodes.CompanyUpdated);

        }

        public async Task<Result<CompanyDto>> GetByIdAsync(long id)
        {
            var company = await ufw.GetRepository<Company>().GetByIdAsync(id, [nameof(Company.Address)]);

            if (company is null) return new NotFoundError();

            var companyDto = mapper.Map<CompanyDto>(company);
            return Result<CompanyDto>.Success(companyDto, SuccessCodes.CompanyReceived);

        }

        public async Task<Result<CompanyDto[]>> GetAllAsync()
        {
            var companies = await ufw.GetRepository<Company>().GetAllAsync([nameof(Company.Address)]);

            var companiesDto = mapper.Map<CompanyDto[]>(companies);
            return Result<CompanyDto[]>.Success(companiesDto, SuccessCodes.CompaniesReceived);

        }

        public async Task<Result<Empty>> SoftDeleteAsync(long id)
        {
            var company = await ufw.GetRepository<Company>().GetByIdAsync(id, [nameof(Company.Address)]);

            if (company == null)
                return new NotFoundError();

            ufw.GetRepository<Company>().SoftDelete(company);
            await ufw.SaveChangesAsync();

            return Result<Empty>.Success(Empty.Default, SuccessCodes.CompanySoftDeleted);
            ;
        }

        public async Task<Result<Empty>> HardDeleteAsync(long id)
        {
            var company = await ufw.GetRepository<Company>().GetByIdAsync(id, [nameof(Company.Address)]);

            if (company == null)
                return new NotFoundError();

            ufw.GetRepository<Company>().HardDelete(company);
            await ufw.SaveChangesAsync();


            return Result<Empty>.Success(Empty.Default, SuccessCodes.CompanyHardDeleted);

        }
    }
}
