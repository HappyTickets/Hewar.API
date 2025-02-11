namespace Domain.Entities.FacilityAggregate;
public class SecurityCertificate : SoftDeletableEntity
{
    public DateTimeOffset StartDate { get; set; }
    public DateTimeOffset EndDate { get; set; }

    public double SiteArea { get; set; }
    public int AgreedNumberOfSecurityGuards { get; set; }

    public long AddressId { get; set; }
    public virtual Address Address { get; set; }

    public int? NumberOfCameras { get; set; }
    public bool HasCentralMonitoringRoom { get; set; }

    public string? ContractDocumentUrl { get; set; }

    public ContractStatus Status { get; set; } = ContractStatus.Pending;

    public long FacilityId { get; set; }

    public virtual Facility Facility { get; set; }


    public bool IsActive()
    {
        return Status == ContractStatus.Verified && EndDate > DateTimeOffset.UtcNow;
    }

    public void ApproveContract()
    {
        Status = ContractStatus.Verified;
    }

    public void RejectContract()
    {
        Status = ContractStatus.Rejected;
    }
}


