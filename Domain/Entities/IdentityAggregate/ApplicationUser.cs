using Microsoft.AspNetCore.Identity;
namespace Domain.Entities.IdentityAggregate;

public class ApplicationUser : IdentityUser<long>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? ImageUrl { get; set; }

    public virtual ICollection<RefreshToken>? RefreshTokens { get; set; }
    public virtual ICollection<ApplicationUserRole>? ApplicationUserRoles { get; set; } = new List<ApplicationUserRole>();


}
