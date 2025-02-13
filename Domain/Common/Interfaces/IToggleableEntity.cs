namespace Domain.Common.Interfaces
{
    public interface IToggleableEntity
    {
        bool IsFacilityHidden { get; set; }
        bool IsCompanyHidden { get; set; }
    }
}
