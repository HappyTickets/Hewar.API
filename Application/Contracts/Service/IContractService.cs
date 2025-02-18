using Application.Contracts.DTOs;
using Application.Contracts.DTOs.Dynamic;

namespace Application.Contracts.Service
{
    public interface IContractService
    {
        Task<Result<long>> CreateContractForOfferAsync(long offerId, ContractFieldsDto contractFields);
        Task<Result<long>> CreateContractForCompanyAsync(long companyId, ContractFieldsDto contractKeys, long? facilityId = null);

        Task<Result<Empty>> UpdateContractByFieldsAsync(long contractId, ContractFieldsDto contractFields);
        Task<Result<Empty>> UpdateContractByKeysAsync(long contractId, List<UpdateContractKeyDto> contractKeys);

        Task<Result<RichContractDto>> GetContractByIdAsync(long contractId);
        Task<Result<RichContractDto>> GetContractByOfferIdAsync(long offerId);

        Task<Result<GetContractKeysDto?>> GetContractKeysByContractIdAsync(long contractId);
        Task<Result<GetContractKeysDto?>> GetContractKeysByOfferIdAsync(long offer);

        Task<Result<Empty>> SignContractAsync(long contractId, string signature);
    }

}


