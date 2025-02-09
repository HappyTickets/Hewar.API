using Application.Common.Dtos;

namespace Application.SecurityContracts.DTOs;

public class SecurityContractDto
{
    public long Id { get; set; }
    public long FacilityId { get; set; }
    public DateTimeOffset StartDate { get; set; }
    public DateTimeOffset EndDate { get; set; }
    public double SiteArea { get; set; }
    public int AgreedNumberOfSecurityGuards { get; set; }
    public int? NumberOfCameras { get; set; }
    public bool HasCentralMonitoringRoom { get; set; }
    public string? ContractDocumentUrl { get; set; }
    public ContractStatus Status { get; set; }
    public bool IsValid => Status == ContractStatus.Verified && EndDate > DateTimeOffset.UtcNow;
    public string FacilityName { get; set; }
    public AddressDto Address { get; set; }
}

