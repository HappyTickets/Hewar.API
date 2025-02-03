﻿using Application.Chats.DTOs;
using Application.PriceRequests.Dtos.Offers;
using Application.PriceRequests.Dtos.Requests;
using Application.PriceRequests.Service;
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
        //[HasAccountType(AccountTypes.Facility)]
        public async Task<IActionResult> CreateRequestAsync(CreatePriceRequestDto dto)
            => Result(await _priceRequestsService.CreateRequestAsync(dto));

        [HttpPatch("acceptRequest")]
        //[HasAccountType(AccountTypes.Company)]
        public async Task<IActionResult> AcceptRequestAsync(CreatePriceOfferDto dto)
            => Result(await _priceRequestsService.AcceptRequestAsync(dto));

        [HttpPatch("rejectRequest")]
        //[HasAccountType(AccountTypes.Company)]
        public async Task<IActionResult> RejectRequestAsync(long priceRequestId)
            => Result(await _priceRequestsService.RejectRequestAsync(priceRequestId));

        [HttpPatch("cancelRequest")]
        //[HasAccountType(AccountTypes.Facility)]
        public async Task<IActionResult> CancelRequestAsync(long priceRequestId)
            => Result(await _priceRequestsService.CancelRequestAsync(priceRequestId));

        [HttpGet("getMyRequestsAsFacility")]
        //[HasAccountType(AccountTypes.Facility)]
        public async Task<IActionResult> GetMyRequestsAsFacilityAsync()
            => Result(await _priceRequestsService.GetMyRequestsAsFacilityAsync());

        [HttpGet("getMyRequestsAsCompany")]
        //[HasAccountType(AccountTypes.Company)]
        public async Task<IActionResult> GetMyRequestsAsCompanyAsync()
            => Result(await _priceRequestsService.GetMyRequestsAsCompanyAsync());
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

        [HttpPost("createChatMessage")]
        //[HaveAccountTypes(AccountTypes.Facility, AccountTypes.Company)]
        public async Task<IActionResult> CreateRequestMessageAsync(CreateChatMessageDto dto)
            => Result(await _priceRequestsService.CreateRequestMessageAsync(dto));

        [HttpPatch("initializeRequestChat")]
        //[HaveAccountTypes(AccountTypes.Facility, AccountTypes.Company)]
        public async Task<IActionResult> InitialzeRequestChatAsync(long priceRequestId)
            => Result(await _priceRequestsService.InitialzePriceRequestChatAsync(priceRequestId));

        [HttpPatch("initializeOfferChat")]
        //[HaveAccountTypes(AccountTypes.Facility, AccountTypes.Company)]
        public async Task<IActionResult> InitialzeOfferChatAsync(long offerId)
            => Result(await _priceRequestsService.InitialzeOfferChatAsync(offerId));

        [HttpGet("getChatMessages")]
        //[HaveAccountTypes(AccountTypes.Facility, AccountTypes.Company)]
        public async Task<IActionResult> GetChatMessagesAsync(long chatId)
            => Result(await _priceRequestsService.GetChatMessagesAsync(chatId));
    }
}
