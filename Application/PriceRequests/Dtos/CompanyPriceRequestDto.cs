using Application.Facilities.Dtos;

namespace Application.PriceRequests.Dtos
{
    public class CompanyPriceRequestDto
    {
        public long Id { get; set; }
        public string SecurityRole { get; set; }
        public int GuardsCount { get; set; }
        public string WorkShift { get; set; }
        public string ContractType { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public PriceRequestOfferDto Response { get; set; }
        public FacilityBreifDto Facility { get; set; }
    }
}
