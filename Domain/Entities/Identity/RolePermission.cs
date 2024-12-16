using Domain.Entities.UserEntities;

namespace Domain.Entities.Identity
{
    public class RolePermission: BaseEntity
    {
        public Permissions Permission { get; set; }

        public long RoleId { get; set; }
        public virtual ApplicationRole Role { get; set; }
    }
}
