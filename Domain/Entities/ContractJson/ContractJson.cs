using Domain.Entities.ContractJson.Nested;

namespace Domain.Entities.ContractJson
{
    public class ContractJson
    {
        public BilingualText ContractTitle { get; set; }
        public DateTime ContractSignDate { get; set; }
        public DateTime ContractStartDate { get; set; }
        public PartyDetails PartyOne { get; set; }
        public PartyDetails PartyTwo { get; set; }
        public Preamble Preamble { get; set; }
        public List<ScheduleEntry> ScheduleEntries { get; set; } = new List<ScheduleEntry>();
        public List<BilingualText> CustomClauses { get; set; } = new List<BilingualText>();
        public Footer Footer { get; set; }
    }

}