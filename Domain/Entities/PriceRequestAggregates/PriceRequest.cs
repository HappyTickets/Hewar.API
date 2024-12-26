namespace Domain.Entities.PriceRequestAggregates
{
    public class PriceRequest : SoftDeletableEntity
    {
        public SecurityRoles SecurityRole { get; set; }
        public int GuardsCount { get; set; }
        public WorkShifts WorkShift { get; set; }
        public ContractTypes ContractType { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public string Description { get; set; }
        public RequestStatus Status { get; set; }
        public long FacilityId { get; set; }
        public long CompanyId { get; set; }

        // nav props
        public Facility Facility { get; set; }
        public Company Company { get; set; }
        public ICollection<Ticket> Tickets { get; set; }
        public PriceRequestOffer Response { get; set; }
        public PriceRequestFacilityDetails FacilityDetails { get; set; }
    }
}
