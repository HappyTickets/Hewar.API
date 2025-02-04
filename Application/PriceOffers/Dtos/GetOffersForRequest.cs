using Application.PriceRequests.Dtos;

namespace Application.PriceOffers.Dtos
{
    public class GetOffersForRequest
    {
        public GetPriceRequestBriefDto PriceRequest { get; set; }
        public ICollection<GetPriceOfferDto> Offers { get; set; }
    }
}
