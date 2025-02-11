namespace Domain.Entities.ContractJson.Nested
{
    public class ScheduleEntry
    {
        public BilingualText Location { get; set; }
        public BilingualText GuardsRequired { get; set; }
        public BilingualText ShiftTime { get; set; }
        public BilingualText Notes { get; set; }
    }
}
