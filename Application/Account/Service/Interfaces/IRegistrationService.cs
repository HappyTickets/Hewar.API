using Domain.Entities.CompanyAggregate;
using Domain.Entities.FacilityAggregate;

namespace Application.Account.Service.Interfaces
{
    public interface IRegistrationService
    {
        public Task<Result<Empty>> ValidateRegistrationAsync(string phone, string email);
        public Task<Result<Empty>> CreateFacilityAsync(Facility facility);
        public Task<Result<Empty>> CreateCompanyAsync(Company company);

        public Task<Result<Empty>> RegisterEntityWithAdminAsync(
        ApplicationUser adminUser,
        string password,
        string roleName,
        Func<Task<Result<Empty>>> entityCreationFunction,
        CancellationToken cancellationToken);
    }
}