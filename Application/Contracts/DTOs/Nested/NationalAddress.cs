namespace Application.Contracts.DTOs
{
    public class NationalAddress
    {
        public BilingualText City { get; set; }
        public string PostalCode { get; set; }
        public string UnitNumber { get; set; }
        public string BuildingNumber { get; set; }
    }
}
