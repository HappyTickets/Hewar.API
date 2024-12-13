namespace Application.AccountManagement.Dtos.Authentication;

public class RegisterGuardRequest
{
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Password { get; set; }
    public string Name { get; set; }
    public DateTimeOffset DateOfBirth { get; set; }
    public string Skills { get; set; }
}
