using Domain.Entities.CompanyAggregate;
using Domain.Entities.FacilityAggregate;

namespace Domain.Entities.MissionAggregate
{
    public class Mission : SoftDeletableEntity
    {
        public long CompanyId { get; set; }
        public virtual Company Company { get; set; }
        public long FacilityId { get; set; }
        public virtual Facility Facility { get; set; }
        public virtual ICollection<MissionGuard> Guards { get; set; } = new List<MissionGuard>();
        public virtual ICollection<CompanyService> Services { get; set; } = new List<CompanyService>();
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public RequestStatus Status { get; set; }
    }

}
