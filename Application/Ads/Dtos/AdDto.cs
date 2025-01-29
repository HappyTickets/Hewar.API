using Application.Facilities.Dtos;

namespace Application.Ads.Dtos
{
    public class AdDto
    {
        public long Id { get; set; }
        public SecurityRoles SecurityRole { get; set; }
        public int GuardsCount { get; set; }
        public ShiftType WorkShift { get; set; }
        public ContractType ContractType { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public string Description { get; set; }
        public AdStatus Status { get; set; }
        public FacilityBreifDto Facility { get; set; }
    }
}
