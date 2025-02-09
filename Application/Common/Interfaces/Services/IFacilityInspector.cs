namespace Application.Common.Interfaces.Services
{
    public interface IFacilityInspector
    {
        public Task<bool> IsAuthorized(long? facilityId);

    }
}
