using Application.Hewar.Dtos;

namespace Application.Hewar.Service
{
    public interface IHewarProvidedService
    {
        Task<Result<long>> CreateAsync(CreateHewarServiceDto dto, CancellationToken cancellationToken = default);
        Task<Result<HewarServiceDto>> GetByIdAsync(long id);
        Task<Result<List<HewarServiceDto>>> GetAllAsync();
        Task<Result<Empty>> UpdateAsync(HewarServiceDto dto);
        Task<Result<Empty>> DeleteAsync(long id);
    }
}
