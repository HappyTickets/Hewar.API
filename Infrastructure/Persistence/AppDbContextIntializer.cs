using LanguageExt;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Infrastructure.Persistence
{
    public class AppDbContextIntializer
    {
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public AppDbContextIntializer(AppDbContext context, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task InitialiseAsync()
        {
            if ((await _context.Database.GetPendingMigrationsAsync()).Count() > 1)
            {
                await _context.Database.MigrateAsync();
            }

            if (!await _userManager.Users.AnyAsync())
            {
                var user = new ApplicationUser
                {
                    FirstName = "Anas",
                    LastName = "Amin",
                    Email = "hema@gmail.com",
                    EmailConfirmed = true,
                };

                await _userManager.CreateAsync(user, "Hema123!");
                await _userManager.AddToRoleAsync(user, Roles.Admin);
                await _userManager.AddClaimAsync(user, new Claim(CustomClaims.UserId, user.Id.ToString()));
            }
        }
    }
}
