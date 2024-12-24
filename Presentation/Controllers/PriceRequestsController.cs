using Application.PriceRequests.Dtos;
using Application.PriceRequests.Service;
using Infrastructure.Authentication.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/priceRequests")]
    [Authorize]
    public class PriceRequestsController : ApiControllerBase
    {
        private readonly IPriceRequestsService _priceRequestsService;

        public PriceRequestsController(IPriceRequestsService priceRequestsService)
        {
            _priceRequestsService = priceRequestsService;
        }

        [HttpPost("createRequest")]
        [HasPermission(Permissions.CreatePriceRequest)]
        public async Task<IActionResult> CreateRequestAsync(CreatePriceRequestDto dto)
            => Result(await _priceRequestsService.CreateRequestAsync(dto));

        [HttpGet("getRequestForFacility")]
        [HasAccountType(AccountTypes.Facility)]
        [HasPermission(Permissions.ViewPriceRequests)]
        public async Task<IActionResult> GetRequestForFacilityAsync()
            => Result(await _priceRequestsService.GetRequestForFacilityAsync());

        [HttpGet("getRequestForCompany")]
        [HasAccountType(AccountTypes.Company)]
        [HasPermission(Permissions.ViewPriceRequests)]
        public async Task<IActionResult> GetRequestForCompanyAsync()
            => Result(await _priceRequestsService.GetRequestForCompanyAsync());
        
        [HttpPost("createFacilityDetails")]
        [HasPermission(Permissions.CreatePriceRequest)]
        public async Task<IActionResult> CreateFacilityDetailsAsync(CreatePriceRequestFacilityDetailsDto dto)
            => Result(await _priceRequestsService.CreateFacilityDetailsAsync(dto));
        [HttpPut("updateFacilityDetails")]
        [HasPermission(Permissions.UpdatePriceRequest)]
        public async Task<IActionResult> UpdateFacilityDetailsAsync(long facilityDetailsId, UpdatePriceRequestFacilityDetailsDto dto)
            => Result(await _priceRequestsService.UpdateFacilityDetailsAsync(facilityDetailsId, dto));

        [HttpGet("getFacilityDetails")]
        [HasPermission(Permissions.ViewPriceRequests)]
        public async Task<IActionResult> GetFacilityDetailsAsync(long priceRequestId)
            => Result(await _priceRequestsService.GetFacilityDetailsAsync(priceRequestId));

        [HttpPatch("acceptRequest")]
        [HasPermission(Permissions.AcceptPriceRequest)]
        public async Task<IActionResult> AcceptRequestAsync(CreatePriceRequestResponseDto dto)
           => Result(await _priceRequestsService.AcceptRequestAsync(dto));

        [HttpPatch("rejectRequest")]
        [HasPermission(Permissions.RejectPriceRequest)]
        public async Task<IActionResult> RejectRequestAsync(long priceRequestId)
            => Result(await _priceRequestsService.RejectRequestAsync(priceRequestId));
    }
}
