namespace Domain.Entities
{
    public class Guard : SoftDeletableEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTimeOffset DateOfBirth { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset? EndDate { get; set; }
        public string Status { get; set; }
        public decimal HourlyRate { get; set; }
        public string Skills { get; set; }
        public int TenantId { get; set; }

        // nav props
        public Tenant Tenant { get; set; }
        public ICollection<Shift> Shifts { get; set; }
        public ICollection<Attendance> Attendances { get; set; }
        public ICollection<PerformanceReview> PerformanceReviews { get; set; }
        public ICollection<Payroll> Payrolls { get; set; }
    }
}
