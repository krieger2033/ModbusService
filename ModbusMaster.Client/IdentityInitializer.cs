using Microsoft.AspNetCore.Identity;
using ModbusMaster.Client.Domain.Entities;

namespace ModbusMaster.Client
{
    public static class IdentityInitializer
    {
        public static void SeedData(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            SeedRoles(roleManager);
            SeedUsers(userManager);
        }

        public static void SeedUsers(UserManager<ApplicationUser> userManager)
        {
            string adminEmail = "d.lekhman@student.csn.khai.edu";
            if (userManager.FindByEmailAsync(adminEmail).Result != null) { return; }

            ApplicationUser user = new ApplicationUser()
            {
                UserName = adminEmail,
                Email = adminEmail,
                EmailConfirmed = true
            };

            PasswordHasher<ApplicationUser> ph = new PasswordHasher<ApplicationUser>();
            user.PasswordHash = ph.HashPassword(user, "a_A12345");

            IdentityResult result = userManager.CreateAsync(user).Result;

            if (result.Succeeded)
            {
                userManager.AddToRoleAsync(user, "Admin").Wait();
            }
        }

        public static void SeedRoles(RoleManager<ApplicationRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync("User").Result)
            {
                ApplicationRole role = new ApplicationRole() { Name = "User" };
                roleManager.CreateAsync(role).Wait();
            }

            if (!roleManager.RoleExistsAsync("Admin").Result)
            {
                ApplicationRole role = new ApplicationRole() { Name = "Admin" };
                roleManager.CreateAsync(role).Wait();
            }
        }
    }
}