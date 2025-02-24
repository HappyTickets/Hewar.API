using Application.Contracts.DTOs.Dynamic;
using Application.Contracts.Service;
using Infrastructure.Authentication.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers.Contracts
{
    [Authorize]
    public class ContractsController(IContractService contractService) : ApiControllerBase
    {
        [HttpPost("CreateContractForOffer")]
        [HasPermission(Permissions.CreateContract)]
        public async Task<IActionResult> CreateContractForOfferAsync(long offerId, [FromBody] ContractFieldsDto contractFields)
        {
            var result = await contractService.CreateContractForOfferAsync(offerId, contractFields);
            return Result(result);
        }

        [HttpPost("CreateContractForCompany")]
        [HasPermission(Permissions.CreateContract)]
        public async Task<IActionResult> CreateContractForCompanyAsync(long companyId, [FromBody] ContractFieldsDto contractFields, long? facilityId = null)
        {
            var result = await contractService.CreateContractForCompanyAsync(companyId, contractFields, facilityId);
            return Result(result);
        }
        [HttpPut("UpdateContractByFields")]
        [HasPermission(Permissions.UpdateContract)]
        public async Task<IActionResult> UpdateContractFieldsAsync(long contractId, [FromBody] ContractFieldsDto contractFields)
        {
            var result = await contractService.UpdateContractByFieldsAsync(contractId, contractFields);
            return Result(result);
        }

        [HttpPut("UpdateContractByKeys")]
        [HasPermission(Permissions.UpdateContract)]
        public async Task<IActionResult> UpdateContractByKeysAsync(long contractId, [FromBody] List<UpdateContractKeyDto> contractKeys)
        {
            var result = await contractService.UpdateContractByKeysAsync(contractId, contractKeys);
            return Result(result);
        }

        [HttpGet("GetById")]
        [HasPermission(Permissions.ViewContracts)]
        public async Task<IActionResult> GetContractByIdAsync(long contractId)
        {
            var result = await contractService.GetContractByIdAsync(contractId);
            return Result(result);
        }

        [HttpGet("GetByOfferId")]
        [HasPermission(Permissions.ViewContracts)]
        public async Task<IActionResult> GetContractByOfferIdAsync(long offerId)
        {
            var result = await contractService.GetContractByOfferIdAsync(offerId);
            return Result(result);
        }

        [HttpGet("GetContractKeysByContractId")]
        [HasPermission(Permissions.ViewContracts)]
        public async Task<IActionResult> GetContractKeysByContractIdAsync(long contractId)
        {
            var result = await contractService.GetContractKeysByContractIdAsync(contractId);
            return Result(result);
        }

        [HttpGet("GetContractKeysByOfferId")]
        [HasPermission(Permissions.ViewContracts)]
        public async Task<IActionResult> GetContractKeysByOfferIdAsync(long offerId)
        {
            var result = await contractService.GetContractKeysByOfferIdAsync(offerId);
            return Result(result);
        }

        [HttpGet("GetContractFieldsByOfferId")]
        [HasPermission(Permissions.ViewContracts)]
        public async Task<IActionResult> GetContractFieldsByOfferIdAsync(long offerId)
        {
            var result = await contractService.GetContractFieldsByOfferIdAsync(offerId);
            return Result(result);
        }

        [HttpGet("GetContractFieldsByContractId")]
        [HasPermission(Permissions.ViewContracts)]
        public async Task<IActionResult> GetContractFieldsByContractIdAsync(long contractId)
        {
            var result = await contractService.GetContractFieldsByContractIdAsync(contractId);
            return Result(result);
        }

        [HttpPatch("SignContract")]
        [HasPermission(Permissions.SignContract)]
        public async Task<IActionResult> SignContractAsync(long contractId, string signature)
        {
            var result = await contractService.SignContractAsync(contractId, signature);
            return Result(result);
        }
    }

}
