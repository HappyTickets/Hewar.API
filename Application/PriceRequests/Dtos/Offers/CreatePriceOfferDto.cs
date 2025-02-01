namespace Application.PriceRequests.Dtos.Offers
{
    public class CreatePriceOfferDto
    {
        public long PriceRequestId { get; set; }
        public ICollection<PriceOfferServiceDto> PricedServices { get; set; }
    }
}