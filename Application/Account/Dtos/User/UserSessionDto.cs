namespace Application.AccountManagement.Dtos.User
{
    public class UserSessionDto
    {
        public long Id { get; set; }
        public string UserName { get; set; }
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
