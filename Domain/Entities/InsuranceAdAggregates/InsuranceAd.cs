namespace Domain.Entities.InsuranceAdAggregates
{
    public class InsuranceAd : SoftDeletableEntity
    {
        public SecurityRoles SecurityRole { get; set; }
        public int GuardsCount { get; set; }
        public WorkShifts WorkShift { get; set; }
        public ContractTypes ContractType { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public string Description { get; set; }
        public InsuranceAdStatus Status { get; set; }

        public long FacilityId { get; set; }

        // nav props
        public Facility Facility { get; set; }
        public ICollection<InsuranceAdOffer> InsuranceAdOffers { get; set; }
    }
}
