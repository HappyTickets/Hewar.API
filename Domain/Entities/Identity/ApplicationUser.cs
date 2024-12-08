using Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities.UserEntities;

public class ApplicationUser : IdentityUser<long>
{
    public virtual ICollection<RefreshToken>? RefreshTokens { get; set; }
    public virtual ICollection<ApplicationUserRole>? ApplicationUserRoles { get; set; } = new List<ApplicationUserRole>();


    public int SoftDeleteCount { get; set; } = 0;

    public long CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; }
    public long? ModifiedBy { get; set; }
    public DateTime? ModifiedDate { get; set; }
}
