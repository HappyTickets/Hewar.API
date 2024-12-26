using System.Security.Claims;

namespace Domain.Consts
{
    public class CustomeClaims
    {
        public const string AccountType = ClaimTypes.Role; //"AccountType"
        public const string Permission = "Permission";
    }
}
