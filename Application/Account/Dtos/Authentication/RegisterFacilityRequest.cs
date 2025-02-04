using Application.Account.Dtos.User;
using Application.Common.Dtos;

namespace Application.AccountManagement.Dtos.Authentication;

public class RegisterFacilityRequest
{
    public string Type { get; set; }
    public string Name { get; set; }
    public string CommercialRegistration { get; set; }
    public string ActivityType { get; set; }
    public string ResponsibleName { get; set; }
    public string ResponsiblePhone { get; set; }
    public string Logo { get; set; }
    public AddressDto Address { get; set; }
    public AdminInfoDto AdminInfo { get; set; }
}