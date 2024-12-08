namespace Application.Authorization.DTOs.Request
{
    public class AssignUserToRolesDto
    {
        public List<string> Roles { get; set; }
        public long UserId { get; set; }
    }
}
