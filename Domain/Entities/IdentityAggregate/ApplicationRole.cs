

using Microsoft.AspNetCore.Identity;

namespace Domain.Entities.IdentityAggregate
{
    public class ApplicationRole : IdentityRole<long>
    {
        public string? Description { get; set; }

        public virtual ICollection<ApplicationUserRole>? ApplicationUserRoles { get; set; }
        public virtual ICollection<RolePermission>? Permissions { get; set; }
    }
}
