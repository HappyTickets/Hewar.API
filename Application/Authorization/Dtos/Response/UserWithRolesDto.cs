namespace Application.Authorization.DTOs.Response
{
    public class UserWithRolesDto
    {
        public long UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }

        public IEnumerable<string> AssignedRoles { get; set; }

    }
}
