using Domain.Entities.PriceRequestAggregates;
using Domain.Entities.TicketAggregates;
using Infrastructure.Persistence.Extensions;
using Infrastructure.Persistence.Seeds;
using MediatR;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Infrastructure.Persistence
{
    public class AppDbContext(DbContextOptions<AppDbContext> options, ICurrentUserService currentUserService, IPublisher publisher) : IdentityDbContext<ApplicationUser, ApplicationRole, long, ApplicationUserClaim, ApplicationUserRole, ApplicationUserLogin, ApplicationRoleClaim, ApplicationUserToken>(options)

    {
        private readonly ICurrentUserService _currentUserService = currentUserService;
        private readonly IPublisher _publisher = publisher;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.AppendGlobalQueryFilter<SoftDeletableEntity>(e => !e.IsDeleted);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.SeedRoles();

            #region UserRolesRelationship
            modelBuilder.Entity<ApplicationUser>(b =>
           {
               b.HasMany(e => e.ApplicationUserRoles)
                   .WithOne(e => e.User)
                   .HasForeignKey(ur => ur.UserId)
                   .IsRequired();

               b.HasQueryFilter(e => !e.IsDeleted);
           });

            modelBuilder.Entity<ApplicationRole>(b =>
            {
                b.HasMany(e => e.ApplicationUserRoles)
                    .WithOne(e => e.Role)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();

                b.HasMany(r => r.Permissions)
                    .WithOne(rp => rp.Role)
                    .HasForeignKey(rp => rp.RoleId)
                    .IsRequired();
            });
            #endregion

        }


        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ChangeTracker.PrepareAddedEntities(_currentUserService);
            ChangeTracker.PrepareModifiedEntities(_currentUserService);

            var affectedRows = await base.SaveChangesAsync(cancellationToken);

            await _publisher.PublishDomainEvents(this);

            return affectedRows;
        }

        #region DbSets
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<ApplicationRole> ApplicationRoles { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<Payroll> Payrolls { get; set; }
        public DbSet<PerformanceReview> PerformanceReviews { get; set; }
        public DbSet<Policy> Policies { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Shift> Shifts { get; set; }
        public DbSet<Facility> Facilities { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Guard> Guards { get; set; }
        public DbSet<PriceRequest> PriceRequests { get; set; }
        public DbSet<PriceRequestFacilityDetails> PriceRequestFacilityDetails { get; set; }
        public DbSet<PriceRequestOffer> PriceRequestOffers { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<TicketMessage> TicketMessages { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<InsuranceAd> InsuranceAds { get; set; }
        public DbSet<InsuranceAdOffer> InsuranceAdOffers { get; set; }
        public DbSet<InsuranceAdOfferMessage> InsuranceAdOfferMessages { get; set; }
        #endregion

    }
}
