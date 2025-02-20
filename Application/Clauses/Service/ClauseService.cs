using Application.Clauses.DTOs;
using Domain.Entities.ContractAggregate.Dynamic;

namespace Application.Clauses.Service
{
    public class ClauseService(IUnitOfWorkService ufw, ICurrentUserService currentUser) : IClauseService
    {
        public async Task<Result<Empty>> CreateCustomClausesAsync(long contractId, List<CreateCustomClauseDto> customClauses)
        {
            var contract = await ufw.GetRepository<Contract>()
                .FirstOrDefaultAsync(c => c.Id == contractId);

            if (contract is null)
                return new NotFoundError();

            var newClauses = customClauses.Select(cc => new CustomClause
            {
                ContractId = contractId,
                AuthorType = currentUser.EntityType,
                HtmlContentAr = cc.HtmlContentAr,
                HtmlContentEn = cc.HtmlContentEn
            }).ToList();

            await ufw.GetRepository<CustomClause>().CreateRangeAsync(newClauses);

            await ufw.SaveChangesAsync();

            return Result<Empty>.Success(Empty.Default, SuccessCodes.CustomClauseCreated);
        }

        public async Task<Result<Empty>> DeleteCustomClauseAsync(long clauseId)
        {
            var customClause = await ufw.GetRepository<CustomClause>().GetByIdAsync(clauseId);

            if (customClause is null)
                return new NotFoundError();

            ufw.GetRepository<CustomClause>().HardDelete(customClause);
            await ufw.SaveChangesAsync();
            return Result<Empty>.Success(Empty.Default, SuccessCodes.CustomClauseDeleted);
        }

        public async Task<Result<Empty>> UpdateCustomClausesAsync(long contractId, List<UpdateCustomClauseDto> customClauses)
        {
            if (!customClauses.Any())
                return Result<Empty>.Success(Empty.Default);

            var contract = await ufw.GetRepository<Contract>()
                .FirstOrDefaultAsync(c => c.Id == contractId,
                    [nameof(Contract.CustomClauses)]);

            if (contract is null || contract.CustomClauses is null)
                return new NotFoundError();

            var updateClauseIds = customClauses.Select(cc => cc.Id).ToList();

            var existingClauseIds = contract.CustomClauses.Select(cc => cc.Id).ToList();

            var invalidClauseIds = updateClauseIds.Except(existingClauseIds).ToList();

            if (invalidClauseIds.Any())
                return new ConflictError();

            var updateLookup = customClauses.ToDictionary(cc => cc.Id);

            foreach (var clause in contract.CustomClauses)
            {
                if (updateLookup.TryGetValue(clause.Id, out var updatedClause))
                {
                    clause.HtmlContentAr = updatedClause.HtmlContentAr;
                    clause.HtmlContentEn = updatedClause.HtmlContentEn;
                }
            }

            await ufw.SaveChangesAsync();
            return Result<Empty>.Success(Empty.Default, SuccessCodes.CustomClauseUpdated);
        }

        public async Task<Result<CustomClauseDto>> GetCustomClauseByIdAsync(long clauseId)
        {
            var clause = await ufw.GetRepository<CustomClause>().GetByIdAsync(clauseId);
            if (clause is null)
                return new NotFoundError();

            var dto = new CustomClauseDto
            {
                Id = clause.Id,
                ContractId = clause.ContractId,
                HtmlContentAr = clause.HtmlContentAr,
                HtmlContentEn = clause.HtmlContentEn,
                AuthorType = clause.AuthorType
            };

            return Result<CustomClauseDto>.Success(dto);
        }

        public async Task<Result<List<CustomClauseDto>>> GetCustomClausesByContractIdAsync(long contractId)
        {
            var clauses = await ufw.GetRepository<CustomClause>().FilterAsync(c => c.ContractId == contractId);
            var dtos = clauses.Select(c => new CustomClauseDto
            {
                Id = c.Id,
                ContractId = c.ContractId,
                HtmlContentAr = c.HtmlContentAr,
                HtmlContentEn = c.HtmlContentEn,
                AuthorType = c.AuthorType
            }).ToList();

            return Result<List<CustomClauseDto>>.Success(dtos);
        }
    }


}

