using Application.PriceRequests.Dtos;
using Application.PriceRequests.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers.PriceRequest
{
    [Authorize]
    public class PriceRequestsController(IPriceRequestsService priceRequestsService) : ApiControllerBase
    {
        [HttpPost("requests")]
        //[HasAccountType(AccountTypes.Facility)]
        public async Task<IActionResult> CreateRequestAsync(CreatePriceRequestDto dto)
            => Result(await priceRequestsService.CreateRequestAsync(dto));

        [HttpPut("update-request")]
        //[HasAccountType(AccountTypes.Facility)]
        public async Task<IActionResult> UpdateRequestAsync(UpdatePriceRequestDto dto)
           => Result(await priceRequestsService.UpdateRequestAsync(dto));



        [HttpPatch("requests/{priceRequestId}/cancel")]
        //[HasAccountType(AccountTypes.Facility)]
        public async Task<IActionResult> CancelRequestAsync(long priceRequestId)
            => Result(await priceRequestsService.CancelRequestAsync(priceRequestId));

        [HttpGet("requests/facility")]
        //[HasAccountType(AccountTypes.Facility)]
        public async Task<IActionResult> GetMyRequestsAsFacilityAsync()
            => Result(await priceRequestsService.GetMyRequestsAsFacilityAsync());

        [HttpGet("requests/company")]
        //[HasAccountType(AccountTypes.Company)]
        public async Task<IActionResult> GetMyRequestsAsCompanyAsync()
            => Result(await priceRequestsService.GetMyRequestsAsCompanyAsync());
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
