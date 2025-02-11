namespace Domain.Entities.ContractJson.Nested
{
    public class PartyDetails
    {
        public BilingualText Name { get; set; }
        public BilingualText MainOfficeCity { get; set; }
        public string CommercialRegistration { get; set; }
        public string PublicSecurityLicense { get; set; }
        public string Telephone { get; set; }
        public string Mobile { get; set; }
        public NationalAddress NationalAddress { get; set; }
        public string RegistrationInSabl { get; set; }
        public string Email { get; set; }
        public BilingualText RepresentativeName { get; set; }
        public BilingualText RepresentativeTitle { get; set; }
        public BilingualText LocationToBeSecured { get; set; } // Optional for PartyTwo
    }
}
