namespace Application.Contracts.DTOs.Nested.Parties
{
    public class PartyTwoDetails : PartyBase
    {
        public BilingualText CommercialRegistrationCity { get; set; } = new();
        public BilingualText LocationToBeSecured { get; set; } = new();
    }

}
