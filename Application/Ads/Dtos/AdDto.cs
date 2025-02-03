using Application.Ads.Dtos.AdServices;
using Application.Facilities.Dtos;

namespace Application.Ads.Dtos
{
    public class AdDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTimeOffset DatePosted { get; set; }
        public AdStatus Status { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public ContractType ContractType { get; set; }

        public ICollection<AdServiceDto> Services { get; set; }

        public FacilityBreifDto Facility { get; set; }
    }
}