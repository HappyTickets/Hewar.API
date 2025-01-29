using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Seeds
{
    internal static class RoleSeeder
    {
        public static ModelBuilder SeedRoles(this ModelBuilder builder)
        {
            // roles
            var admin = new ApplicationRole
            {
                Id = 1,
                Name = Roles.Admin,
                NormalizedName = Roles.Admin.ToUpper()
            };

            //var company = new ApplicationRole
            //{
            //    Id = 2,
            //    Name = Roles.Company,
            //    NormalizedName = Roles.Company.ToUpper()
            //};

            //var facility = new ApplicationRole
            //{
            //    Id = 3,
            //    Name = Roles.Facility,
            //    NormalizedName = Roles.Facility.ToUpper()
            //};

            //var guard = new ApplicationRole
            //{
            //    Id = 4,
            //    Name = Roles.Guard,
            //    NormalizedName = Roles.Guard.ToUpper()
            //};

            builder.Entity<ApplicationRole>()
                .HasData(admin);
            //.HasData(admin, company, facility, guard);

            // permissions
            var rolePermissions = new RolePermission[]
            {
                    // admin
                    new() { Id = 1, Permission = Permissions.CreateRole, RoleId = admin.Id },
                    new() { Id = 2, Permission = Permissions.UpdateRole, RoleId = admin.Id },
                    new() { Id = 3, Permission = Permissions.DeleteRole, RoleId = admin.Id },
                    new() { Id = 4, Permission = Permissions.ViewRoles, RoleId = admin.Id },
                    new() { Id = 5, Permission = Permissions.AssignUserToRole, RoleId = admin.Id },
                    new() { Id = 6, Permission = Permissions.UnassignUserToRole, RoleId = admin.Id },

                    new() { Id = 7, Permission = Permissions.CreateCompany, RoleId = admin.Id },
                    new() { Id = 8, Permission = Permissions.UpdateCompany, RoleId = admin.Id },
                    new() { Id = 9, Permission = Permissions.DeleteCompany, RoleId = admin.Id },
                    new() { Id = 10, Permission = Permissions.ViewCompanies, RoleId = admin.Id },

                    new() { Id = 11, Permission = Permissions.CreateFacility, RoleId = admin.Id },
                    new() { Id = 12, Permission = Permissions.UpdateFacility, RoleId = admin.Id },
                    new() { Id = 13, Permission = Permissions.DeleteFacility, RoleId = admin.Id },
                    new() { Id = 14, Permission = Permissions.ViewFacilities, RoleId = admin.Id },

                    new() { Id = 15, Permission = Permissions.CreateGuard, RoleId = admin.Id },
                    new() { Id = 16, Permission = Permissions.UpdateGuard, RoleId = admin.Id },
                    new() { Id = 17, Permission = Permissions.DeleteGuard, RoleId = admin.Id },
                    new() { Id = 18, Permission = Permissions.ViewGuards, RoleId = admin.Id },
            };

            builder.Entity<RolePermission>()
                .HasData(rolePermissions);

            return builder;
        }
    }
}
