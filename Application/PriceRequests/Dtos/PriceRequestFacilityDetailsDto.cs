namespace Application.PriceRequests.Dtos
{
    public class PriceRequestFacilityDetailsDto
    {
        public long Id { get; set; }

        // facility details
        public string FacilityName { get; set; }
        public string FacilityEmail { get; set; }
        public string FacilityPhone { get; set; }
        public string FacilityAddress { get; set; }
        public string FacilitySize { get; set; }
        public string FacilityActivityType { get; set; }
        public string FacilityCommercialRegistrationNumber { get; set; }
        public DateTimeOffset FacilityCommercialRegistrationExpiryDate { get; set; }
        public string FacilityLicenseNumber { get; set; }
        public DateTimeOffset FacilityLicenseExpiryDate { get; set; }
        public string? FacilityNotes { get; set; }

        // representative details
        public string RepresentativeName { get; set; }
        public string RepresentativeEmail { get; set; }
        public string RepresentativePhone { get; set; }
        public string RepresentativeNationalId { get; set; }
        public string RepresentativeNationality { get; set; }
        public string? RepresentativeNotes { get; set; }

        // Commissioner's details
        public string CommissionerName { get; set; }
        public string CommissionerEmail { get; set; }
        public string CommissionerPhone { get; set; }
        public string CommissionerNationalId { get; set; }
        public string CommissionerNationality { get; set; }
        public string? CommissionerNotes { get; set; }
    }
}
