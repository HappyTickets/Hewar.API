namespace Application.Authorization.DTOs.Request
{
    public class AssignUserToRolesDto
    {
        public List<string> RolesNames { get; set; }
        public long UserId { get; set; }
    }
}
