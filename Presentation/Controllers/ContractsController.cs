using Application.Contracts.DTOs;
using Application.Contracts.DTOs.Dynamic;
using Application.Contracts.Service;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class ContractsController(IContractService contractService) : ApiControllerBase
    {

        [HttpPost("fillFields")]
        public async Task<IActionResult> CreateContractAsync([FromBody] FillContractFieldsDto dto)
           => Result(await contractService.FillContractFieldsAsync(dto));


        [HttpPut("updateFields")]
        public async Task<IActionResult> UpdateContractAsync([FromBody] UpdateContractFieldsDto dto)
            => Result(await contractService.UpdateContractFieldsAsync(dto));


        [HttpPatch("signContract")]
        public async Task<IActionResult> SignContractAsync(long contractId, string signature)
            => Result(await contractService.SignContractAsync(contractId, signature));

        [HttpGet("getFieldsByContractId")]
        public async Task<IActionResult> GetContractByIdAsync(long contractId)
             => Result(await contractService.GetContractFieldsByIdAsync(contractId));


        [HttpGet("getContractTemplateById")]
        public async Task<IActionResult> GetContractTemplateByIdAsync(long contractId)
            => Result(await contractService.GetContractTemplateByIdAsync(contractId));

        [HttpGet("getContractTemplateByOfferId")]
        public async Task<IActionResult> GetContractTemplateOfferIdAsync(long offerId)
            => Result(await contractService.GetContractTemplateByOfferIdAsync(offerId));


        [HttpGet("getContractFieldsByOfferId")]
        public async Task<IActionResult> GetContractFieldsByOfferIdAsync(long offerId)
         => Result(await contractService.GetContractFieldsByOfferIdAsync(offerId));
    }
}
