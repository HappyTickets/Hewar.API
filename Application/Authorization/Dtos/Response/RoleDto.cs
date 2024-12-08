namespace Application.Authorization.DTOs.Response
{
    public class RoleDto
    {
        public long RoleId { get; set; }
        public required string RoleName { get; set; }
        public required string RoleDescription { get; set; }

    }
}
