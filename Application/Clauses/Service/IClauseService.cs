using Application.Clauses.DTOs;

namespace Application.Clauses.Service
{
    public interface IClauseService
    {
        Task<Result<Empty>> CreateCustomClausesAsync(long contractId, List<CreateCustomClauseDto> customClauses);
        Task<Result<Empty>> UpdateCustomClausesAsync(long contractId, List<CustomClauseDto> customClauses);
        Task<Result<Empty>> DeleteCustomClauseAsync(long clauseId);
    }
}
