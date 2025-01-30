using Application.Common.Dtos;

namespace Application.Facilities.Dtos
{
    public class FacilityDto
    {
        public long Id { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string CommercialRegistration { get; set; }
        public string ActivityType { get; set; }
        public string ResponsibleName { get; set; }
        public string ResponsiblePhone { get; set; }
        public AddressDto Address { get; set; }
    }
}
