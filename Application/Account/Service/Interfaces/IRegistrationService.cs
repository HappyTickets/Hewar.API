namespace Application.Account.Service.Interfaces
{
    public interface IRegistrationService
    {
        public Task<Result<Empty>> ValidateRegistrationAsync(string phone, string email, string role);
        public ApplicationUser CreateUserBase(string email, string phone, AccountTypes accountType, string imageUrl, bool isConfirmed = false);

        public Task<Result<Empty>> RegisterAccountAsync(ApplicationUser user, string password, string role);
    }
}
