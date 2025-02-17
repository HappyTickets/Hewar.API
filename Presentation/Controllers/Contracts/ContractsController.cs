using Application.Contracts.DTOs.nEW;
using Application.Contracts.Service;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers.Contracts
{
    public class ContractsController(IContractService contractService) : ApiControllerBase
    {
        [HttpPost("CreateContractForOffer")]
        public async Task<IActionResult> CreateContractForOfferAsync(long offerId, [FromBody] ContractFieldsDto contractFields)
        {
            var result = await contractService.CreateContractForOfferAsync(offerId, contractFields);
            return Result(result);
        }

        [HttpPost("CreateContractForCompany")]
        public async Task<IActionResult> CreateContractForCompanyAsync(long companyId, [FromBody] ContractFieldsDto contractFields, long? facilityId = null)
        {
            var result = await contractService.CreateContractForCompanyAsync(companyId, contractFields, facilityId);
            return Result(result);
        }
        [HttpPut("UpdateContractByFields")]
        public async Task<IActionResult> UpdateContractFieldsAsync(long contractId, [FromBody] ContractFieldsDto contractFields)
        {
            var result = await contractService.UpdateContractByFieldsAsync(contractId, contractFields);
            return Result(result);
        }

        [HttpPut("UpdateContractByKeys")]
        public async Task<IActionResult> UpdateContractByKeysAsync(long contractId, [FromBody] List<UpdateContractKeyDto> contractKeys)
        {
            var result = await contractService.UpdateContractByKeysAsync(contractId, contractKeys);
            return Result(result);
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetContractByIdAsync(long contractId)
        {
            var result = await contractService.GetContractByIdAsync(contractId);
            return Result(result);
        }

        [HttpGet("GetByOfferId")]
        public async Task<IActionResult> GetContractByOfferIdAsync(long offerId)
        {
            var result = await contractService.GetContractByOfferIdAsync(offerId);
            return Result(result);
        }

        [HttpGet("GetContractKeysByContractId")]
        public async Task<IActionResult> GetContractKeysByContractIdAsync(long contractId)
        {
            var result = await contractService.GetContractKeysByContractIdAsync(contractId);
            return Result(result);
        }

        [HttpGet("GetContractKeysByOfferId")]
        public async Task<IActionResult> GetContractKeysByOfferIdAsync(long offer)
        {
            var result = await contractService.GetContractKeysByOfferIdAsync(offer);
            return Result(result);
        }

        [HttpPatch("SignContract")]
        public async Task<IActionResult> SignContractAsync(long contractId, string signature)
        {
            var result = await contractService.SignContractAsync(contractId, signature);
            return Result(result);
        }
    }

}
