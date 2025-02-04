using Application.Account.Dtos.User;
using Application.Common.Dtos;

namespace Application.AccountManagement.Dtos.Authentication;

public class RegisterCompanyRequest
{
    public string ContactEmail { get; set; }
    public string PhoneNumber { get; set; }
    public string RegistrationNumber { get; set; }
    public string TaxId { get; set; }
    public string Logo { get; set; }

    public string Name { get; set; }
    public AdminInfoDto AdminInfo { get; set; }
    public AddressDto Address { get; set; }
}
