namespace Application.Authorization.DTOs.Request
{
    public class EditRoleDto
    {
        public long RoleId { get; set; }
        public string? RoleName { get; set; }
        public string? RoleDescription { get; set; }
        public string[] Permissions { get; set; }

    }
}
