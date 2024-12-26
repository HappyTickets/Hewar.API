using Domain.Entities.Owned;
using Domain.Entities.UserEntities;

namespace Domain.Entities
{
    public class Guard : SoftDeletableEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTimeOffset DateOfBirth { get; set; }
        public string NationalId { get; set; }
        public Qualifications Qualification { get; set; }
        public Cities City { get; set; }
        public string? BloodType { get; set; }
        public double? Height { get; set; }
        public double? Weight { get; set; }
        public ICollection<Skill>? Skills { get; set; }
        public ICollection<PrevCompany>? PrevCompanies { get; set; }


        public long LoginDetailsId { get; set; }


        // nav props
        public ApplicationUser LoginDetails { get; set; }
        public ICollection<Shift> Shifts { get; set; }
        public ICollection<Attendance> Attendances { get; set; }
        public ICollection<PerformanceReview> PerformanceReviews { get; set; }
        public ICollection<Payroll> Payrolls { get; set; }
        public ICollection<Report> Reports { get; set; }
    }
}
