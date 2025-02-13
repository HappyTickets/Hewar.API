namespace Application.SecurityCertificates.DTOs;
public class SecurityCertificateStatusChangeDto
{
    public long SecurityCertificateId { get; set; }
    public ContractStatus Status { get; set; }
}
