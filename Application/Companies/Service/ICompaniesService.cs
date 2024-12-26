using Application.Companies.Dtos;

namespace Application.Companies.Service
{
    public interface ICompaniesService
    {
        Task<Result<Empty>> CreateAsync(CreateCompanyDto dto);
        Task<Result<CompanyDto[]>> GetAllAsync();
        Task<Result<CompanyDto>> GetByIdAsync(long id);
        Task<Result<Empty>> HardDeleteAsync(long id);
        Task<Result<Empty>> SoftDeleteAsync(long id);
        Task<Result<Empty>> UpdateAsync(UpdateCompanyDto dto);
    }
}
