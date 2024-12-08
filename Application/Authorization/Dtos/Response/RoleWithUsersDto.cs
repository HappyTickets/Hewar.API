using Application.AccountManagement.Dtos.User;
using Application.Common.Utilities.Pagination;

namespace Application.Authorization.DTOs.Response
{
    public class RoleWithUsersDto
    {
        public long RoleId { get; set; }
        public string RoleName { get; set; }
        public string Description { get; set; }
        public PaginatedList<ApplicationUserDTO> AssignedUsers { get; set; }
    }

}
