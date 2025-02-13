namespace Application.Contracts.DTOs
{
    public class ScheduleEntry
    {
        public BilingualText Location { get; set; } = new();
        public BilingualText GuardsRequired { get; set; } = new();
        public BilingualText ShiftTime { get; set; } = new();
        public BilingualText Notes { get; set; } = new();
    }
}
