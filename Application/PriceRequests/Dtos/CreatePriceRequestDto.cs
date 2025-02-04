using Application.PriceRequests.Dtos.Services;

namespace Application.PriceRequests.Dtos
{
    public class CreatePriceRequestDto
    {
        public long CompanyId { get; set; }
        public ContractType ContractType { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public string? Notes { get; set; }
        public ICollection<ServiceRequestDto> Services { get; set; }
        public ICollection<CreateOtherServiceDto>? OtherServices { get; set; }

    }
}
