namespace Application.AccountManagement.Dtos.Authentication;

public class RegisterGuardRequest
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Password { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTimeOffset DateOfBirth { get; set; }
    public string Skills { get; set; }
}
