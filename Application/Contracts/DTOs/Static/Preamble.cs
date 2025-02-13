namespace Application.Contracts.DTOs.Static
{
    public class PartyDescription
    {
        public BilingualText Description { get; set; }
    }
    public class Parties
    {
        public PartyDescription FirstParty { get; set; }
        public PartyDescription SecondParty { get; set; }
    }

    public class Preamble
    {
        public BilingualText Title { get; set; }
        public Parties Parties { get; set; }
        public BilingualText Introduction { get; set; }
        public List<BilingualText> Conditions { get; set; }
    }

}
