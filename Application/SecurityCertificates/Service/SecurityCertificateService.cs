using Application.SecurityCertificates.DTOs;
using AutoMapper;
using Domain.Entities.FacilityAggregate;

namespace Application.SecurityCertificates.Service
{
    public class SecurityCertificateService
        (IUnitOfWorkService ufw,
        ICurrentUserService currentUser,
        IMapper mapper) : ISecurityCertificateService
    {

        public async Task<Result<long>> CreateAsync(SecurityCertificateCreateDto createDto)
        {
            var securityCertificate = mapper.Map<SecurityCertificate>(createDto);
            securityCertificate.FacilityId = currentUser.EntityId!.Value;
            securityCertificate.Status = ContractStatus.Pending;
            await ufw.GetRepository<SecurityCertificate>().CreateAsync(securityCertificate);
            await ufw.SaveChangesAsync();
            return Result<long>.Success(securityCertificate.Id, SuccessCodes.ContractCreated);
        }
        public async Task<Result<Empty>> UpdateAsync(SecurityCertificateUpdateDto updateDto)
        {
            var existingContract = await ufw.GetRepository<SecurityCertificate>()
                .FirstOrDefaultAsync(c => c.Id == updateDto.Id, [nameof(SecurityCertificate.Address)]);

            if (existingContract == null)
                return new NotFoundError();

            mapper.Map(updateDto, existingContract);

            await ufw.SaveChangesAsync();
            return Result<Empty>.Success(Empty.Default, SuccessCodes.ContractUpdated);
        }
        public async Task<Result<Empty>> DeleteAsync(long id)
        {
            var contract = await ufw.GetRepository<SecurityCertificate>()
                .FirstOrDefaultAsync(c => c.Id == id);

            if (contract is null)
                return new NotFoundError();

            ufw.GetRepository<SecurityCertificate>().HardDelete(contract);

            await ufw.SaveChangesAsync();
            return Result<Empty>.Success(Empty.Default, SuccessCodes.ContractDeleted);
        }

        public async Task<Result<SecurityCertificateDto[]>> GetByFacilityIdAsync(long facilityId)
        {
            var contracts = await ufw.GetRepository<SecurityCertificate>()
                .FilterAsync<SecurityCertificateDto>(sc => sc.FacilityId == facilityId);

            return Result<SecurityCertificateDto[]>
                .Success(contracts.ToArray(), SuccessCodes.OperationSuccessful);
        }
        public async Task<Result<SecurityCertificateDto>> GetByIdAsync(long id)
        {
            var contract = await ufw.GetRepository<SecurityCertificate>()
                .FirstOrDefaultAsync<SecurityCertificateDto>(c => c.Id == id);

            if (contract is null)
                return new NotFoundError();

            return Result<SecurityCertificateDto>.Success(contract, SuccessCodes.OperationSuccessful);
        }

        public async Task<Result<Empty>> ApproveAsync(long id)
        {
            var contract = await ufw.GetRepository<SecurityCertificate>()
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
            var contract = await ufw.GetRepository<SecurityCertificate>()
                .FirstOrDefaultAsync(c => c.Id == id);

            if (contract is null)
                return new NotFoundError();

            if (contract.Status != ContractStatus.Pending)
                return new ConflictError(ErrorCodes.ContractNotPending);

            contract.RejectContract();
            await ufw.SaveChangesAsync();
            return Result<Empty>.Success(Empty.Default, SuccessCodes.ContractRejected);
        }

        public async Task<Result<SecurityCertificateDto[]>> GetAllAsync()
            => (await ufw.GetRepository<SecurityCertificate>()
            .GetAllAsync<SecurityCertificateDto>()).ToArray();
    }
}
