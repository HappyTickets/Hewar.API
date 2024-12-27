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
        [HasAccountType(AccountTypes.Facility)]
        public async Task<IActionResult> CreateRequestAsync(CreatePriceRequestDto dto)
            => Result(await _priceRequestsService.CreateRequestAsync(dto));

        [HttpPatch("acceptRequest")]
        [HasAccountType(AccountTypes.Company)]
        public async Task<IActionResult> AcceptRequestAsync(CreatePriceRequestOfferDto dto)
            => Result(await _priceRequestsService.AcceptRequestAsync(dto));

        [HttpPatch("rejectRequest")]
        [HasAccountType(AccountTypes.Company)]
        public async Task<IActionResult> RejectRequestAsync(long priceRequestId)
            => Result(await _priceRequestsService.RejectRequestAsync(priceRequestId));

        [HttpGet("getMyRequestsAsFacility")]
        [HasAccountType(AccountTypes.Facility)]
        public async Task<IActionResult> GetMyRequestsAsFacilityAsync()
            => Result(await _priceRequestsService.GetMyRequestsAsFacilityAsync());

        [HttpGet("getMyRequestsAsCompany")]
        [HasAccountType(AccountTypes.Company)]
        public async Task<IActionResult> GetMyRequestsAsCompanyAsync()
            => Result(await _priceRequestsService.GetMyRequestsAsCompanyAsync());

        [HttpPost("createRequestFacilityDetails")]
        [HasAccountType(AccountTypes.Facility)]
        public async Task<IActionResult> CreateRequestFacilityDetailsAsync(CreatePriceRequestFacilityDetailsDto dto)
            => Result(await _priceRequestsService.CreateRequestFacilityDetailsAsync(dto));

        [HttpPut("updateRequestFacilityDetails")]
        [HasAccountType(AccountTypes.Facility)]
        public async Task<IActionResult> UpdateRequestFacilityDetailsAsync(long facilityDetailsId, UpdatePriceRequestFacilityDetailsDto dto)
            => Result(await _priceRequestsService.UpdateRequestFacilityDetailsAsync(facilityDetailsId, dto));

        [HttpGet("getRequestFacilityDetails")]
        [HaveAccountTypes(AccountTypes.Facility, AccountTypes.Company)]
        public async Task<IActionResult> GetRequestFacilityDetailsAsync(long priceRequestId)
            => Result(await _priceRequestsService.GetRequestFacilityDetailsAsync(priceRequestId));
    }
}
