namespace Application.PriceRequests.Dtos.Offers
{
    public class PriceOfferDto
    {
        public long Id { get; set; }
        public long PriceRequestId { get; set; }
        public ICollection<PriceOfferServiceDto> PricedServices { get; set; }
    }
}
