using Infrastructure.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Infrastructure.Persistence
{
    internal class AppDbContext : DbContext
    {
        private readonly ICurrentUserService _currentUserService;
        public AppDbContext(DbContextOptions options, ICurrentUserService currentUserService) : base(options)
        {
            _currentUserService = currentUserService;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.AppendGlobalQueryFilter<SoftDeletableEntity>(e => !e.IsDeleted);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ChangeTracker.PrepareAddedEntities(_currentUserService);
            ChangeTracker.PrepareModifiedEntities(_currentUserService);

            return await base.SaveChangesAsync(cancellationToken);
        }

        #region DbSets
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Guard> Guards { get; set; }
        public DbSet<Payroll> Payrolls { get; set; }
        public DbSet<PerformanceReview> PerformanceReviews { get; set; }
        public DbSet<Policy> Policies { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Shift> Shifts { get; set; }
        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        #endregion

    }
}
