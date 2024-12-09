using Domain.Entities.UserEntities;

namespace Domain.Entities
{
    public class Guard : SoftDeletableEntity
    {
        public string Name { get; set; }
        public DateTimeOffset DateOfBirth { get; set; }
        public string Skills { get; set; }
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
