namespace Application.AccountManagement.Dtos.Authentication;

public class RegisterFacilityRequest
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Password { get; set; }
    public string Type { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string CommercialRegistration { get; set; }
    public string ActivityType { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string ResponsibleName { get; set; }
    public string ResponsiblePhone { get; set; }
}
