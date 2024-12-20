﻿using Microsoft.AspNetCore.Identity;
using ST10355869_PROG6212_Part2.Services;

namespace ST10355869_PROG6212_Part2.Data
{
    public class DatabaseSeeder
    {
        public static async Task SeedRolesAndUsers(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var editLecturerService = serviceProvider.GetRequiredService<EditLecturer>();


            string[] roles = { "Admin", "User" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            var adminUser = new IdentityUser
            {
                UserName = "manager1",
                Email = "manager@example.com",
                EmailConfirmed = true // Confirm the email
            };

            if (userManager.Users.All(u => u.UserName != adminUser.UserName))
            {
                var result = await userManager.CreateAsync(adminUser, "Manager@123");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }

            var normalUser = new IdentityUser
            {
                UserName = "user",
                Email = "user@example.com",
                EmailConfirmed = true // Confirm the email
            };

            if (userManager.Users.All(u => u.UserName != normalUser.UserName))
            {
                var result = await userManager.CreateAsync(normalUser, "User@123");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(normalUser, "User");
                }
            }


            await editLecturerService.SeedEditLecturerModelsAsync();


        }

    }
}