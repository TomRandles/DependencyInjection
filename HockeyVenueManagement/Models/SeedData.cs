using System;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using HockeyVenueManagement.Models;
using HockeyVenueManagement.Model;

namespace HockeyVenueManagement.Data
{
    public static class SeedData
    {
        private const string AdminRole = "Admin";

        public static async Task SeedUsersAndRoles(UserManager<HockeyBookingsUser> userManager, RoleManager<HockeyBookingsRole> roleManager)
        {
            if (!await roleManager.RoleExistsAsync(AdminRole))
            {
                var adminRole = new HockeyBookingsRole { Name = AdminRole };
                await roleManager.CreateAsync(adminRole);
            }

            if (await userManager.FindByEmailAsync("admin@example.com") == null)
            {
                var user = new HockeyBookingsUser
                {
                    UserName = "admin@example.com",
                    Email = "admin@example.com"
                };

                var result = await userManager.CreateAsync(user, "Password1!");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, AdminRole);
                }
            }

            if (await userManager.FindByEmailAsync("member@example.com") == null)
            {
                var user = new HockeyBookingsUser
                {
                    UserName = "member@example.com",
                    Email = "member@example.com",
                    Member = new Member
                    {
                        Forename = "Tom",
                        Surname = "Randles",
                        JoinDate = DateTime.UtcNow.Date
                    }
                };

                await userManager.CreateAsync(user, "Password1!");
            }
        }
    }
}
