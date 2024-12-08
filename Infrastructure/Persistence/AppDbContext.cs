﻿using Domain.Entities.Identity;
using Domain.Entities.UserEntities;
using Infrastructure.Persistence.Extensions;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Infrastructure.Persistence
{
    internal class AppDbContext(DbContextOptions<AppDbContext> options, ICurrentUserService currentUserService) : IdentityDbContext<ApplicationUser, ApplicationRole, long, ApplicationUserClaim, ApplicationUserRole, ApplicationUserLogin, ApplicationRoleClaim, ApplicationUserToken>(options)

    {
        private readonly ICurrentUserService _currentUserService = currentUserService;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.AppendGlobalQueryFilter<SoftDeletableEntity>(e => !e.IsDeleted);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());


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
            });
            #endregion

        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ChangeTracker.PrepareAddedEntities(_currentUserService);
            ChangeTracker.PrepareModifiedEntities(_currentUserService);

            return await base.SaveChangesAsync(cancellationToken);
        }

        #region DbSets
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<ApplicationRole> ApplicationRoles { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

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
