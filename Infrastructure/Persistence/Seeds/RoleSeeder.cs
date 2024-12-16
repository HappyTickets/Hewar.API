using Domain.Entities.UserEntities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Seeds
{
    internal static class RoleSeeder
    {
        public static ModelBuilder SeedRoles(this ModelBuilder builder)
        {
            var admin = new ApplicationRole
            {
                Name = "Admin",
                Permissions =
                [
                    // users
                    new() { Permission = Permissions.CreateUser },
                    new() { Permission = Permissions.UpdateUser },
                    new() { Permission = Permissions.DeleteUser },
                    new() { Permission = Permissions.ViewUsers },

                    // roles
                    new() { Permission = Permissions.CreateRole },
                    new() { Permission = Permissions.UpdateRole },
                    new() { Permission = Permissions.DeleteRole },
                    new() { Permission = Permissions.ViewRoles },
                    new() { Permission = Permissions.AssignUserToRole },
                    new() { Permission = Permissions.UnassignUserToRole },
                ]
            };

            var company = new ApplicationRole
            {
                Name = "Company",
                Permissions =
                [
                    // price requests
                    new() { Permission = Permissions.ViewPriceRequests },
                    new() { Permission = Permissions.AcceptPriceRequest },
                    new() { Permission = Permissions.RejectPriceRequest },

                    //tickets
                    new() { Permission = Permissions.ViewTickets },
                    new() { Permission = Permissions.CloseTicket },
                    new() { Permission = Permissions.CreateTicketMessage },
                    new() { Permission = Permissions.ViewTicketMessages },
                ]
            };

            var facility = new ApplicationRole
            {
                Name = "Facility",
                Permissions =
                [
                    // price requests
                    new() { Permission = Permissions.CreatePriceRequest },
                    new() { Permission = Permissions.UpdatePriceRequest },
                    new() { Permission = Permissions.ViewPriceRequests },

                    //tickets
                    new() { Permission = Permissions.CreateTicket },
                    new() { Permission = Permissions.ViewTickets },
                    new() { Permission = Permissions.CreateTicketMessage },
                    new() { Permission = Permissions.ViewTicketMessages },
                ]
            };

            var guard = new ApplicationRole
            {
                Name = "Guard",
                Permissions =
                [

                ]
            };

            builder.Entity<ApplicationRole>()
                .HasData(admin, company, facility, guard);

            return builder;
        }
    }
}
