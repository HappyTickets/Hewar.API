using Application.PriceRequests.Dtos;
using Application.PriceRequests.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers.PriceRequest
{
    [Authorize]
    public class PriceRequestsController(IPriceRequestsService priceRequestsService) : ApiControllerBase
    {
        [HttpPost("create")]
        //[HasAccountType(AccountTypes.Facility)]
        public async Task<IActionResult> CreateRequestAsync(CreatePriceRequestDto dto)
            => Result(await priceRequestsService.CreateRequestAsync(dto));

        [HttpPut("update")]
        //[HasAccountType(AccountTypes.Facility)]
        public async Task<IActionResult> UpdateRequestAsync(UpdatePriceRequestDto dto)
           => Result(await priceRequestsService.UpdateRequestAsync(dto));



        [HttpPatch("cancel")]
        //[HasAccountType(AccountTypes.Facility)]
        public async Task<IActionResult> CancelRequestAsync(long priceRequestId)
            => Result(await priceRequestsService.CancelRequestAsync(priceRequestId));

        [HttpGet("getMyFacilityRequests")]
        //[HasAccountType(AccountTypes.Facility)]
        public async Task<IActionResult> GetMyFacilityRequestsAsync()
            => Result(await priceRequestsService.GetMyFacilityRequestsAsync());

        [HttpGet("getMyCompanyRequests")]
        //[HasAccountType(AccountTypes.Company)]
        public async Task<IActionResult> GetMyCompanyRequestsAsync()
            => Result(await priceRequestsService.GetMyCompanyRequestsAsync());

        #region Facility Details Later

        //[HttpPost("createRequestFacilityDetails")]
        ////[HasAccountType(AccountTypes.Facility)]
        //public async Task<IActionResult> CreateRequestFacilityDetailsAsync(CreatePriceRequestFacilityDetailsDto dto)
        //    => Result(await _priceRequestsService.CreateRequestFacilityDetailsAsync(dto));

        //[HttpPut("updateRequestFacilityDetails")]
        ////[HasAccountType(AccountTypes.Facility)]
        //public async Task<IActionResult> UpdateRequestFacilityDetailsAsync(long facilityDetailsId, UpdatePriceRequestFacilityDetailsDto dto)
        //    => Result(await _priceRequestsService.UpdateRequestFacilityDetailsAsync(facilityDetailsId, dto));

        //[HttpGet("getRequestFacilityDetails")]
        ////[HaveAccountTypes(AccountTypes.Facility, AccountTypes.Company)]
        //public async Task<IActionResult> GetRequestFacilityDetailsAsync(long priceRequestId)
        //    => Result(await _priceRequestsService.GetRequestFacilityDetailsAsync(priceRequestId)); 
        #endregion
    }
}
