namespace Application.Contracts.DTOs.Nested.Parties
{
    public class PartyOneDetails : PartyBase
    {
        public string CommercialRegistration { get; set; } = string.Empty;
        public string RegistrationInSabl { get; set; } = string.Empty;
        public int GuardsCount { get; set; }

    }

}
