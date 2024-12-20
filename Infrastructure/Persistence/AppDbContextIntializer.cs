﻿using Domain.Entities.UserEntities;
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
            await _context.Database.MigrateAsync();

            if(!await _userManager.Users.AnyAsync())
            {
                var user = new ApplicationUser
                {
                    Email = "hema@gmail.com",
                    EmailConfirmed = true,
                    AccountType = AccountTypes.Admin
                };

                await _userManager.CreateAsync(user, "Hema123!");
                await _userManager.AddToRoleAsync(user, Roles.Admin);
                await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
            }
        }
    }
}
