using Application.Contracts.DTOs.Nested.Parties;
using System.Text.Json.Serialization;

namespace Application.Contracts.DTOs
{
    public class ContractFields1
    {
        [JsonIgnore]
        public long OfferNumber { get; set; }
        [JsonIgnore]
        public DateTime OfferDate { get; set; }
        public DateTime ContractSignDate { get; set; }
        public DateTime ContractStartDate { get; set; }

        public PartyOneDetails PartyOne { get; set; } = new(); // شركة
        public PartyTwoDetails PartyTwo { get; set; } = new(); // مؤسسة
        public List<ScheduleEntry> ScheduleEntries { get; set; } = new List<ScheduleEntry>();
        public List<BilingualText> CustomClauses { get; set; } = new List<BilingualText>(); // HTml
    }

}