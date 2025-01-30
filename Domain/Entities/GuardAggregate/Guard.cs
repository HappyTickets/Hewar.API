namespace Domain.Entities.GuardAggregate
{
    public class Guard : ApplicationUser
    {
        public DateTimeOffset DateOfBirth { get; set; }
        public string NationalId { get; set; }
        public Qualifications Qualification { get; set; }
        public Cities City { get; set; }

        public long AddressID { get; set; }
        public virtual Address Address { get; set; }

        public BloodType? BloodType { get; set; }
        public double? Height { get; set; }
        public double? Weight { get; set; }

        public virtual ICollection<Skill>? Skills { get; set; } = new List<Skill>();
        public virtual ICollection<PrevCompany>? PrevCompanies { get; set; } = new List<PrevCompany>();

        public virtual ICollection<Shift> Shifts { get; set; } = new List<Shift>();
        public virtual ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();
        public virtual ICollection<PerformanceReview> PerformanceReviews { get; set; } = new List<PerformanceReview>();
        public virtual ICollection<Payroll> Payrolls { get; set; } = new List<Payroll>();
        public virtual ICollection<Report> Reports { get; set; } = new List<Report>();
    }
}
