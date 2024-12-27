
using Microsoft.AspNetCore.Identity;
namespace Domain.Entities.IdentityAggregates;

public class ApplicationUser : IdentityUser<long>
{
    public string ImageUrl { get; set; }
    public AccountTypes AccountType { get; set; }
    public virtual ICollection<RefreshToken>? RefreshTokens { get; set; }
    public virtual ICollection<ApplicationUserRole>? ApplicationUserRoles { get; set; } = new List<ApplicationUserRole>();

    public bool IsDeleted { get; set; }

    public Guard? Guard { get; set; }
    public Company? Company { get; set; }
    public Facility? Facility { get; set; }
}
