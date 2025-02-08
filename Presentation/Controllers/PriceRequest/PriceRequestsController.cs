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


        [HttpGet("getById")]
        public async Task<IActionResult> GetRequestByIdAsync(long priceRequestId)
           => Result(await priceRequestsService.GetByIdAsync(priceRequestId));

        [HttpPatch("cancel")]
        //[HasAccountType(AccountTypes.Facility)]
        public async Task<IActionResult> CancelRequestAsync(long priceRequestId)
            => Result(await priceRequestsService.CancelRequestAsync(priceRequestId));

        [HttpPatch("hide")]
        //[HasAccountType(AccountTypes.Facility)] or company 
        public async Task<IActionResult> HidePriceRequestAsync(long priceRequestId)
            => Result(await priceRequestsService.HideRequestAsync(priceRequestId));

        [HttpPatch("show")]
        //[HasAccountType(AccountTypes.Facility)] or company 
        public async Task<IActionResult> ShowPriceRequestAsync(long priceRequestId)
            => Result(await priceRequestsService.ShowRequestAsync(priceRequestId));

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
