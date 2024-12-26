namespace Application.Guards.Dtos
{
    public class UpdateGuardDto
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ImageUrl { get; set; }
        public DateTimeOffset DateOfBirth { get; set; }
        public string NationalId { get; set; }
        public Qualifications Qualification { get; set; }
        public Cities City { get; set; }
        public string? BloodType { get; set; }
        public double? Height { get; set; }
        public double? Weight { get; set; }
        public ICollection<SkillDto>? Skills { get; set; }
        public ICollection<PrevCompanyDto>? PrevCompanies { get; set; }
    }
}
