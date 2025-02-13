namespace Application.Contracts.DTOs.Nested.Parties
{
    public class PartyBase
    {
        public BilingualText Name { get; set; } = new();
        public BilingualText MainOfficeCity { get; set; } = new();
        public string PublicSecurityLicense { get; set; } = string.Empty;
        public string Telephone { get; set; } = string.Empty;
        public string Mobile { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public NationalAddress NationalAddress { get; set; } = new();
        public BilingualText RepresentativeName { get; set; } = new();
        public BilingualText RepresentativeTitle { get; set; } = new();
    }

}
