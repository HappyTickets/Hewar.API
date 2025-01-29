using Application.Common.Dtos;

namespace Application.Companies.Dtos
{
    public class UpdateCompanyDto
    {
        public long Id { get; set; }
        public string ContactEmail { get; set; }
        public string PhoneNumber { get; set; }
        public string RegistrationNumber { get; set; }
        public string TaxId { get; set; }
        public string Name { get; set; }

        public AddressDto Address { get; set; }
    }
}
