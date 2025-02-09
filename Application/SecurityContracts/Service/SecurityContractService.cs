using Application.SecurityContracts.DTOs;
using AutoMapper;
using Domain.Entities.FacilityAggregate;

namespace Application.SecurityContracts.Service
{
    public class SecurityContractService
        (IUnitOfWorkService ufw,
        ICurrentUserService currentUser,
        IMapper mapper) : ISecurityContractService
    {

        public async Task<Result<long>> CreateAsync(SecurityContractCreateDto createDto)
        {
            var securityContract = mapper.Map<SecurityContract>(createDto);
            securityContract.FacilityId = currentUser.EntityId!.Value;
            securityContract.Status = ContractStatus.Pending;
            await ufw.GetRepository<SecurityContract>().CreateAsync(securityContract);
            await ufw.SaveChangesAsync();
            return Result<long>.Success(securityContract.Id, SuccessCodes.ContractCreated);
        }
        public async Task<Result<Empty>> UpdateAsync(SecurityContractUpdateDto updateDto)
        {
            var existingContract = await ufw.GetRepository<SecurityContract>()
                .FirstOrDefaultAsync(c => c.Id == updateDto.Id, [nameof(SecurityContract.Address)]);

            if (existingContract == null)
                return new NotFoundError();

            mapper.Map(updateDto, existingContract);

            await ufw.SaveChangesAsync();
            return Result<Empty>.Success(Empty.Default, SuccessCodes.ContractUpdated);
        }
        public async Task<Result<Empty>> DeleteAsync(long id)
        {
            var contract = await ufw.GetRepository<SecurityContract>()
                .FirstOrDefaultAsync(c => c.Id == id);

            if (contract is null)
                return new NotFoundError();

            ufw.GetRepository<SecurityContract>().HardDelete(contract);

            await ufw.SaveChangesAsync();
            return Result<Empty>.Success(Empty.Default, SuccessCodes.ContractDeleted);
        }

        public async Task<Result<SecurityContractDto[]>> GetByFacilityIdAsync(long facilityId)
        {
            var contracts = await ufw.GetRepository<SecurityContract>()
                .FilterAsync<SecurityContractDto>(sc => sc.FacilityId == facilityId);

            return Result<SecurityContractDto[]>
                .Success(contracts.ToArray(), SuccessCodes.OperationSuccessful);
        }
        public async Task<Result<SecurityContractDto>> GetByIdAsync(long id)
        {
            var contract = await ufw.GetRepository<SecurityContract>()
                .FirstOrDefaultAsync<SecurityContractDto>(c => c.Id == id);

            if (contract is null)
                return new NotFoundError();

            return Result<SecurityContractDto>.Success(contract, SuccessCodes.OperationSuccessful);
        }

        public async Task<Result<Empty>> ApproveAsync(long id)
        {
            var contract = await ufw.GetRepository<SecurityContract>()
                .FirstOrDefaultAsync(c => c.Id == id);
            if (contract is null) return new NotFoundError();

            if (contract.Status != ContractStatus.Pending)
                return new ConflictError(ErrorCodes.ContractNotPending);

            contract.ApproveContract();
            await ufw.SaveChangesAsync();
            return Result<Empty>.Success(Empty.Default, SuccessCodes.ContractVerified);

        }
        public async Task<Result<Empty>> RejectAsync(long id)
        {
            var contract = await ufw.GetRepository<SecurityContract>()
                .FirstOrDefaultAsync(c => c.Id == id);

            if (contract is null)
                return new NotFoundError();

            if (contract.Status != ContractStatus.Pending)
                return new ConflictError(ErrorCodes.ContractNotPending);

            contract.RejectContract();
            await ufw.SaveChangesAsync();
            return Result<Empty>.Success(Empty.Default, SuccessCodes.ContractRejected);
        }

    }
}
