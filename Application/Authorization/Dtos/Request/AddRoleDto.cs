using System.ComponentModel.DataAnnotations;

namespace Application.Authorization.DTOs.Request
{
    public class AddRoleDto
    {
        [Required]
        public string? RoleName { get; set; }
        public string? RoleDescription { get; set; }
        public Permissions[] Permissions { get; set; }
    }
}
