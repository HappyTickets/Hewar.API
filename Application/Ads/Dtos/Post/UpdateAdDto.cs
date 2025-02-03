using Application.Ads.Dtos.AdServices;

namespace Application.Ads.Dtos.Post
{
    public class UpdateAdDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public ContractType ContractType { get; set; }

        public AdStatus Status { get; set; }
        public ICollection<AdServiceDto> Services { get; set; }
    }
}
