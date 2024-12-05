using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Extensions
{
    internal static class ChangeTrackerExtensions
    {
        public static void PrepareAddedEntities(this ChangeTracker changeTracker, ICurrentUserService user)
        {
            var entries = changeTracker
                .Entries<AuditableEntity>()
                .Where(e => e.State == EntityState.Added);

            foreach (var entry in entries)
            {
                entry.Entity.CreatedBy = user.Id;
                entry.Entity.CreatedOn = DateTimeOffset.UtcNow;
            }
        }

        public static void PrepareModifiedEntities(this ChangeTracker changeTracker, ICurrentUserService user)
        {
            var entries = changeTracker
                .Entries<AuditableEntity>()
                .Where(e => e.State == EntityState.Modified);

            foreach (var entry in entries)
            {
                entry.Entity.ModifiedBy = user.Id;
                entry.Entity.ModifiedOn = DateTimeOffset.UtcNow;
            }
        }
    }
}
