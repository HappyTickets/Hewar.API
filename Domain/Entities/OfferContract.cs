namespace Domain.Entities
{
    public class BilingualText
    {
        public string Ar { get; set; }
        public string En { get; set; }
    }
    public class ContractTemplate
    {
        public BilingualText ContractTitle { get; set; }
        public PartyDetails PartyOne { get; set; }
        public PartyDetails PartyTwo { get; set; }

        public Preamble Preamble { get; set; }

        //public List<ServiceOffer> SecuritySchedule { get; set; } = new List<ServiceOffer>();

        public List<Clause> Clauses { get; set; } = new List<Clause>();
        public List<CustomClause> CustomClauses { get; set; } = new List<CustomClause>();

        public Footer Footer { get; set; }
    }
    public class PartyDetails
    {
        public string Name { get; set; }
        public string MainOfficeCity { get; set; }
        public string CommercialRegistration { get; set; }
        public string PublicSecurityLicense { get; set; }
        public string Telephone { get; set; }
        public string Mobile { get; set; }
        public string NationalAddress { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string UnitNumber { get; set; }
        public string BuildingNumber { get; set; }
        public string RegistrationInSabl { get; set; }
        public string Email { get; set; }
        public string RepresentativeName { get; set; }
        public string RepresentativeTitle { get; set; }
    }
    public class Preamble
    {
        public string OfferNumber { get; set; }
        public DateTime OfferDate { get; set; }
        public string ServiceDescription { get; set; }
    }
    public class Clause
    {
        public int ClauseNumber { get; set; }
        public BilingualText Text { get; set; }
    }
    public class CustomClause
    {
        public string CustomClauseAr { get; set; }
        public string CustomClauseEn { get; set; }
    }
    public class Footer
    {
        public BilingualText ClosingRemark { get; set; }
        public BilingualText PartyOneSignature { get; set; }
        public BilingualText PartyTwoSignature { get; set; }
    }

}
