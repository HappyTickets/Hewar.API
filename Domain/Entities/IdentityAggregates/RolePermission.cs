using Microsoft.AspNetCore.Identity;

namespace Domain.Entities.IdentityAggregates
{
    public class RolePermission: BaseEntity
    {
        public Permissions Permission { get; set; }

        public long RoleId { get; set; }
        public virtual ApplicationRole Role { get; set; }
    }
}
