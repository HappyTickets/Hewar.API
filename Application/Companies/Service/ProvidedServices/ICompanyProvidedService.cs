using Application.Companies.Dtos.ProvidedServices;

namespace Application.Companies.Service.ServicesProvided
{
    public interface ICompanyProvidedService
    {
        Task<Result<long>> CreateAsync(CreateCompanyServiceDto dto, CancellationToken cancellationToken = default);
        Task<Result<CompanyServiceDto>> GetByIdAsync(long id);
        Task<Result<List<CompanyServiceDto>>> GetAllAsync();
        Task<Result<Empty>> UpdateAsync(UpdateCompanyServiceDto dto);
        Task<Result<Empty>> DeleteAsync(long id);
    }
}
