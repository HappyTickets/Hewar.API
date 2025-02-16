using Application.Contracts.DTOs;
using Application.Contracts.DTOs.Dynamic;

namespace Application.Contracts.Service
{
    public interface IContractService
    {
        Task<Result<long>> FillContractFieldsAsync(FillContractFieldsDto dto);
        Task<Result<Empty>> UpdateContractFieldsAsync(UpdateContractFieldsDto dto);
        Task<Result<GetContractFieldsDto>> GetContractFieldsByIdAsync(long contractId);
        Task<Result<ContractDto?>> GetContractTemplateByIdAsync(long contractId);
        Task<Result<ContractDto?>> GetContractTemplateByOfferIdAsync(long offerId);
        Task<Result<GetContractFieldsDto>> GetContractFieldsByOfferIdAsync(long offerId);
        Task<Result<Empty>> SignContractAsync(long contractId, string signature);
    }

}
