namespace Application.Authorization.DTOs.Response
{
    public class RoleDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string[] Permissions { get; set; }

    }
}
