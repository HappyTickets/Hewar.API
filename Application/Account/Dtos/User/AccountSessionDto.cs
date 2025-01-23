using System.Text.Json.Serialization;

namespace Application.AccountManagement.Dtos.User
{
    public class AccountSessionDto
    {
        public long IdentityId { get; set; }
        public long AccountId { get; set; } // Guard Id , company id  , etc 


        #region Depends on acc type 
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? UserName { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? FirstName { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Name { get; set; }
        #endregion

        public string Email { get; set; }
        public AccountTypes AccountType { get; set; }
        public string ImageUrl { get; set; }
        public Permissions[] Permissions { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime AccessTokenExpDate { get; set; }
        public DateTime RefreshTokenExpDate { get; set; }
    }
}
