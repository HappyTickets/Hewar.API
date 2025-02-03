using Domain.Entities.CompanyAggregate;

namespace Domain.Entities.PriceRequestAggregates
{
    public class ServiceRequest
    {
        public long Id { get; set; }
        public long PriceRequestId { get; set; }
        public PriceRequest PriceRequest { get; set; }
        public long ServiceId { get; set; }
        public CompanyService Service { get; set; }
        public int Quantity { get; set; }
        public ShiftType ShiftType { get; set; }
    }

}
