using Application.Contracts.DTOs.nEW;
using Application.Contracts.Service;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class ContractsController(IContractService contractService) : ApiControllerBase
    {


        [HttpPost("CreateContractForOffer")]
        public async Task<IActionResult> CreateContractForOfferAsync(long offerId, [FromBody] ContractFieldsDto contractFields)
           => Ok("جاري العمل");

        [HttpPost("CreateContractForCompany")]
        public async Task<IActionResult> CreateContractForCompanyAsync(long companyId, ContractFieldsDto contractKeys, long? facilityId = null) => Ok("تحت الانشاء");


        [HttpPut("UpdateContractByFields")]
        public async Task<IActionResult> UpdateContractFieldsAsync(long contractId, [FromBody] ContractFieldsDto contractFields) => Ok("تحت الانشاء");

        [HttpPut("UpdateContractByKeys")]
        public async Task<IActionResult> UpdateContractByKeysAsync(long contractId, [FromBody] List<UpdateContractKeyDto> contractKeys) => Ok("تحت الانشاء");


        [HttpGet("GetById")]
        public async Task<IActionResult> GetContractByIdAsync(long contractId)
            => Ok(new RichContractDto());

        [HttpGet("GetByOfferId")]
        public async Task<IActionResult> GetContractByOfferIdAsync(long offerId)
            => Ok(new RichContractDto());

        [HttpGet("GetContractKeysByContractId")]
        public async Task<IActionResult> GetContractKeysByContractIdAsync(long contractId)
        => Ok(new GetContractKeysDto());

        [HttpGet("GetContractKeysByOfferId")]
        public async Task<IActionResult> GetContractKeysByOfferIdAsync(long offer)
        => Ok(new GetContractKeysDto());


        [HttpPatch("signContract")]
        public async Task<IActionResult> SignContractAsync(long contractId, string signature)
                => Ok("تحت الانشاء");


        //[HttpPost("fillFields")]
        //public async Task<IActionResult> CreateContractAsync([FromBody] FillContractFieldsDto dto)
        //   => Result(await contractService.FillContractFieldsAsync(dto));


        //[HttpPut("updateFields")]
        //public async Task<IActionResult> UpdateContractAsync([FromBody] UpdateContractFieldsDto dto)
        //    => Result(await contractService.UpdateContractFieldsAsync(dto));




        //[HttpGet("getFieldsByContractId")]
        //public async Task<IActionResult> GetContractByIdAsync(long contractId)
        //     => Result(await contractService.GetContractFieldsByIdAsync(contractId));


        //[HttpGet("getContractTemplateById")]
        //public async Task<IActionResult> GetContractTemplateByIdAsync(long contractId)
        //    => Result(await contractService.GetContractTemplateByIdAsync(contractId));

        //[HttpGet("getContractTemplateByOfferId")]
        //public async Task<IActionResult> GetContractTemplateOfferIdAsync(long offerId)
        //    => Result(await contractService.GetContractTemplateByOfferIdAsync(offerId));


        //[HttpGet("getContractFieldsByOfferId")]
        //public async Task<IActionResult> GetContractFieldsByOfferIdAsync(long offerId)
        // => Result(await contractService.GetContractFieldsByOfferIdAsync(offerId));
    }
}
