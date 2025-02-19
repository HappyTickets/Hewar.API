namespace Application.ScheduleEntries.DTOs
{
    public class CreateScheduleEntryDto
    {
        public string LocationAr { get; set; }
        public string LocationEn { get; set; }
        public int GuardsRequired { get; set; }
        public string ShiftTimeAr { get; set; }
        public string ShiftTimeEn { get; set; }
        public string? NotesAr { get; set; }
        public string? NotesEn { get; set; }
    }
}
