using Domain.Common.Interfaces;
using Domain.Entities.CompanyAggregate;
using Domain.Entities.ContractAggregate.Dynamic;
using Domain.Entities.ContractAggregate.Static;
using Domain.Entities.FacilityAggregate;
using Infrastructure.Persistence.Extensions;
using Infrastructure.Persistence.Seeds;
using Infrastructure.Persistence.Seeds.Contract;
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

            modelBuilder.AppendGlobalQueryFilter<IToggleableEntity>(e =>
            (_currentUserService.EntityType == EntityTypes.Facility && !e.IsFacilityHidden)
            || (_currentUserService.EntityType == EntityTypes.Company && !e.IsCompanyHidden));

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.SeedRoles().SeedStaticContract();

            #region UserRolesRelationship
            modelBuilder.Entity<ApplicationUser>(b =>
           {
               b.HasMany(e => e.ApplicationUserRoles)
                   .WithOne(e => e.User)
                   .HasForeignKey(ur => ur.UserId)
                   .IsRequired();

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
        public DbSet<PriceOffer> PriceRequestOffers { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<TicketMessage> TicketMessages { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<Ad> Ads { get; set; }
        public DbSet<AdOffer> AdOffers { get; set; }
        public DbSet<Contract> Contracts { get; set; }

        public DbSet<StaticContract> StaticContracts { get; set; }
        public DbSet<StaticClause> StaticClauses { get; set; }

        #endregion

    }
}
