using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.UserEntities;

public class RefreshToken : BaseEntity
{

    [Required]
    public required string Token { get; set; }
    [Required]
    public DateTimeOffset CreationDate { get; set; }
    [Required]
    public DateTimeOffset ExpiryDate { get; set; }
    [Required]
    public long UserId { get; set; }
    public DateTimeOffset? RevokedOn { get; set; }
    public bool IsActive => RevokedOn == null && !IsExpired;
    public bool IsExpired => DateTime.UtcNow >= ExpiryDate;


    [Required]
    [ForeignKey(nameof(UserId))]
    public virtual ApplicationUser? User { get; set; }
}
