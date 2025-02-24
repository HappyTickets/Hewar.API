using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Seeds
{
    internal static class RoleSeeder
    {
        public static ModelBuilder SeedRoles(this ModelBuilder builder)
        {
            // roles
            var superAdmin = new ApplicationRole
            {
                Id = 1,
                Name = Roles.SuperAdmin,
                NormalizedName = Roles.SuperAdmin.ToUpper()
            };

            builder.Entity<ApplicationRole>()
                .HasData(superAdmin);

            return builder;
        }
    }
}
