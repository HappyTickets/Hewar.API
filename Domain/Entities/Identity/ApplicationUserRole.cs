using Domain.Entities.UserEntities;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities.Identity;

public class ApplicationUserRole : IdentityUserRole<long>
{
    public virtual ApplicationUser? User { get; set; }
    public virtual ApplicationRole? Role { get; set; }
}