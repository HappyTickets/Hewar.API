namespace Application.Authorization.DTOs.Request
{
    public class AssignUsersToRoleDto
    {
        public long RoleId { get; set; }
        public List<long> UserIds { get; set; }
    }
}
