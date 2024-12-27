using Microsoft.AspNetCore.Identity;

namespace Domain.Entities.IdentityAggregates;

public class ApplicationUserRole : IdentityUserRole<long>
{
    public virtual ApplicationUser? User { get; set; }
    public virtual ApplicationRole? Role { get; set; }
}