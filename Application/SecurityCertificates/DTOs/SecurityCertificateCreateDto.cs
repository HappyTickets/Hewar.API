using Application.Common.Dtos;

namespace Application.SecurityCertificates.DTOs
{
    public class SecurityCertificateCreateDto
    {
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public double SiteArea { get; set; }
        public int AgreedNumberOfSecurityGuards { get; set; }
        public AddressDto Address { get; set; }
        public int? NumberOfCameras { get; set; }
        public bool HasCentralMonitoringRoom { get; set; }
        public string? ContractDocumentUrl { get; set; }
    }
}
