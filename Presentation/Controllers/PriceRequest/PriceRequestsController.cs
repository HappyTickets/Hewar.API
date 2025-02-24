using Application.PriceRequests.Dtos;
using Application.PriceRequests.Service;
using Infrastructure.Authentication.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers.PriceRequest
{
    [Authorize]
    public class PriceRequestsController(IPriceRequestsService priceRequestsService) : ApiControllerBase
    {
        [HttpPost("create")]
        [AnyEntityType(EntityTypes.Facility)]
        [ServiceFilter(typeof(IsVerifiedFacilityAttribute))]
        [HasPermission(Permissions.CreatePriceRequest)]
        public async Task<IActionResult> CreateRequestAsync(CreatePriceRequestDto dto)
            => Result(await priceRequestsService.CreateRequestAsync(dto));

        [HttpPut("update")]
        [AnyEntityType(EntityTypes.Facility)]
        [HasPermission(Permissions.UpdatePriceRequest)]
        public async Task<IActionResult> UpdateRequestAsync(UpdatePriceRequestDto dto)
           => Result(await priceRequestsService.UpdateRequestAsync(dto));


        [HttpGet("getById")]
        [HasPermission(Permissions.ViewPriceRequests)]
        public async Task<IActionResult> GetRequestByIdAsync(long priceRequestId)
           => Result(await priceRequestsService.GetByIdAsync(priceRequestId));

        [HttpPatch("cancel")]
        [AnyEntityType(EntityTypes.Facility)]
        [ServiceFilter(typeof(IsVerifiedFacilityAttribute))]
        public async Task<IActionResult> CancelRequestAsync(long priceRequestId)
            => Result(await priceRequestsService.CancelRequestAsync(priceRequestId));

        [HttpPatch("hide")]
        [AnyEntityType(EntityTypes.Facility, EntityTypes.Company)]
        [HasPermission(Permissions.HidePriceRequest)]
        public async Task<IActionResult> HidePriceRequestAsync(long priceRequestId)
            => Result(await priceRequestsService.HideRequestAsync(priceRequestId));

        [HttpPatch("show")]
        [AnyEntityType(EntityTypes.Facility, EntityTypes.Company)]
        [HasPermission(Permissions.ShowPriceRequest)]
        public async Task<IActionResult> ShowPriceRequestAsync(long priceRequestId)
            => Result(await priceRequestsService.ShowRequestAsync(priceRequestId));

        [HttpGet("getMyFacilityRequests")]
        [AnyEntityType(EntityTypes.Facility)]
        [HasPermission(Permissions.ViewPriceRequests)]
        public async Task<IActionResult> GetMyFacilityRequestsAsync()
            => Result(await priceRequestsService.GetMyFacilityRequestsAsync());

        [HttpGet("getMyCompanyRequests")]
        [AnyEntityType(EntityTypes.Company)]
        [HasPermission(Permissions.ViewPriceRequests)]
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
