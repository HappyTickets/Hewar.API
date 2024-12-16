namespace Application.AccountManagement.Dtos.Authentication;

public class RegisterCompanyRequest
{
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Password { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
}
