using System.Text.Json.Serialization;

namespace Application.AccountManagement.Dtos.User
{
    public class AccountSessionDto
    {
        public long UserId { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? UserName { get; set; }

        public string? FirstName { get; set; }
        public string Email { get; set; }
        public string ImageUrl { get; set; }
        public Permissions[] Permissions { get; set; }
        public string AccessToken { get; set; }
        public DateTime AccessTokenExpDate { get; set; }

    }
}
