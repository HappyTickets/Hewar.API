using Domain.Entities.Identity;
using Domain.Entities.UserEntities;
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

            var company = new ApplicationRole
            {
                Id = 2,
                Name = Roles.Company,
                NormalizedName = Roles.Company.ToUpper()
            };

            var facility = new ApplicationRole
            {
                Id = 3,
                Name = Roles.Facility,
                NormalizedName = Roles.Facility.ToUpper()
            };

            var guard = new ApplicationRole
            {
                Id = 4,
                Name = Roles.Guard,
                NormalizedName = Roles.Guard.ToUpper()
            };

            builder.Entity<ApplicationRole>()
                .HasData(admin, company, facility, guard);

            // permissions
            var rolePermissions = new RolePermission[]
            {
                    // admin
                    new() { Id = 1, Permission = Permissions.CreateUser, RoleId = admin.Id },
                    new() { Id = 2, Permission = Permissions.UpdateUser, RoleId = admin.Id },
                    new() { Id = 3, Permission = Permissions.DeleteUser, RoleId = admin.Id },
                    new() { Id = 4, Permission = Permissions.ViewUsers, RoleId = admin.Id },

                    new() { Id = 5, Permission = Permissions.CreateRole, RoleId = admin.Id },
                    new() { Id = 6, Permission = Permissions.UpdateRole, RoleId = admin.Id },
                    new() { Id = 7, Permission = Permissions.DeleteRole, RoleId = admin.Id },
                    new() { Id = 8, Permission = Permissions.ViewRoles, RoleId = admin.Id },
                    new() { Id = 9, Permission = Permissions.AssignUserToRole, RoleId = admin.Id },
                    new() { Id = 10, Permission = Permissions.UnassignUserToRole, RoleId = admin.Id },

                    // company
                    new() { Id = 11, Permission = Permissions.ViewPriceRequests, RoleId = company.Id },
                    new() { Id = 12, Permission = Permissions.AcceptPriceRequest, RoleId = company.Id },
                    new() { Id = 13, Permission = Permissions.RejectPriceRequest, RoleId = company.Id },

                    new() { Id = 14, Permission = Permissions.ViewTickets, RoleId = company.Id },
                    new() { Id = 15, Permission = Permissions.CloseTicket, RoleId = company.Id },
                    new() { Id = 16, Permission = Permissions.CreateTicketMessage, RoleId = company.Id },
                    new() { Id = 17, Permission = Permissions.ViewTicketMessages, RoleId = company.Id },

                    // facility
                    new() { Id = 18, Permission = Permissions.CreatePriceRequest, RoleId = facility.Id },
                    new() { Id = 19, Permission = Permissions.UpdatePriceRequest, RoleId = facility.Id },
                    new() { Id = 20, Permission = Permissions.ViewPriceRequests, RoleId = facility.Id },

                    new() { Id = 21, Permission = Permissions.CreateTicket, RoleId = facility.Id },
                    new() { Id = 22, Permission = Permissions.ViewTickets, RoleId = facility.Id },
                    new() { Id = 23, Permission = Permissions.CreateTicketMessage, RoleId = facility.Id },
                    new() { Id = 24, Permission = Permissions.ViewTicketMessages, RoleId = facility.Id }

                    // guard

            };

            builder.Entity<RolePermission>()
                .HasData(rolePermissions);

            return builder;
        }
    }
}
