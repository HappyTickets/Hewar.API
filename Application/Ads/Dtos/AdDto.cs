using Application.Ads.Dtos.AdServices.Res;
using Application.Facilities.Dtos;

namespace Application.Ads.Dtos
{
    public class AdDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public DateTimeOffset DatePosted { get; set; }
        public AdStatus Status { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public ContractType ContractType { get; set; }

        public ICollection<GetAdHewarServiceDto> Services { get; set; }
        public ICollection<GetOtherAdServiceDto>? OtherServices { get; set; }
        public FacilityBreifDto Facility { get; set; }
    }
}