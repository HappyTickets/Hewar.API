using LanguageExt;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class AppDbContextIntializer(AppDbContext context, UserManager<ApplicationUser> userManager)
    {
        public async Task InitialiseAsync()
        {
            if ((await context.Database.GetPendingMigrationsAsync()).Count() > 1)
            {
                await context.Database.MigrateAsync();
            }

            if (!await userManager.Users.AnyAsync())
            {
                var user = new ApplicationUser
                {
                    FirstName = "Anas",
                    LastName = "Amin",
                    Email = "SuperAdmin@Hewar.com",
                    EmailConfirmed = true,
                };

                await userManager.CreateAsync(user, "Hewar@1234");
                await userManager.AddToRoleAsync(user, Roles.SuperAdmin);
            }
        }
    }
}
