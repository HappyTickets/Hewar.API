namespace Application.Facilities.Dtos
{
    public class UpdateFacilityDto
    {
        public long Id { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string CommercialRegistration { get; set; }
        public string ActivityType { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string ResponsibleName { get; set; }
        public string ResponsiblePhone { get; set; }
    }
}
