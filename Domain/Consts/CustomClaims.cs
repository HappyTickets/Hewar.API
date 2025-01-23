using System.Security.Claims;

namespace Domain.Consts
{
    public class CustomClaims
    {
        public const string AccountType = ClaimTypes.Role; //"AccountType"
        public const string Permission = "Permission";
        public const string AccountId = nameof(AccountId);
        public const string IdentityId = nameof(IdentityId);
        public const string FirstName = nameof(FirstName);
        public const string UserName = nameof(UserName);
        public const string Name = nameof(Name);
    }
}
