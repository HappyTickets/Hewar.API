using Domain.Entities.CompanyAggregate;
using Domain.Entities.FacilityAggregate;

namespace Application.Account.Service.Interfaces
{
    public interface IRegistrationService
    {
        public Task<Result<Empty>> ValidateRegistrationAsync(string phone, string email);
        public Task<Tuple<long, EntityTypes>> CreateFacilityAsync(Facility facility);
        public Task<Tuple<long, EntityTypes>> CreateCompanyAsync(Company company);

        public Task<Result<Empty>> RegisterEntityWithAdminAsync(
        ApplicationUser adminUser,
        string password,
        string roleName,
        Func<Task<Tuple<long, EntityTypes>>> entityCreationFunction,
        CancellationToken cancellationToken);

    }
}