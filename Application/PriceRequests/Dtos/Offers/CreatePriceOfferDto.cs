﻿using Application.PriceRequests.Dtos.Offers.Services;

namespace Application.PriceRequests.Dtos.Offers
{
    public class CreatePriceOfferDto
    {
        public long PriceRequestId { get; set; }
        public ICollection<ServiceOfferDto> Services { get; set; }
        public ICollection<CreateOtherServiceOfferDto>? OtherServices { get; set; }
    }
}