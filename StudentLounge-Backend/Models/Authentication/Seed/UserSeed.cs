﻿using Microsoft.AspNetCore.Identity;

namespace StudentLounge_Backend.Models.Authentication.Seed
{
    public class UserSeed
    {
        public static void AddDefaultRoles(RoleManager<IdentityRole> roleManager)
        {
            IdentityRole role =  roleManager.FindByIdAsync("1").Result;
            if (role == null)
            {
                var add = roleManager.CreateAsync(new IdentityRole("Admin")).Result;    
                add = roleManager.CreateAsync(new IdentityRole("Student")).Result;
            }
        }

        public static void AddDefaultUser(UserManager<AppUser> userManager)
        {
            AddAdmin(userManager);
            AddStudent(userManager);
        }

        private static void AddAdmin(UserManager<AppUser> userManager)
        {
            string email = "admin@studentlounge.com";
            string root123SlashHash = "a9e8ca9a1eb3297b3d885a24ab07828d9d967c1c428821b1a7f8ccc072f361e1";
            AppUser user = userManager.FindByEmailAsync(email).Result;
            if (user == null)
            {
                var admin = new AppUser()
                {
                    Email = email,
                    UserName = email,
                    Firstname = "Cool",
                    Lastname = "Admin"
                };

                var add = userManager.CreateAsync(admin, root123SlashHash).Result;

                if (add != null)
                {
                    var addRole = userManager.AddToRoleAsync(admin, "Admin").Result;
                }
            }
        }

        private static void AddStudent(UserManager<AppUser> userManager)
        {
            string email = "student@studentlounge.com";
            string root123SlashHash = "a9e8ca9a1eb3297b3d885a24ab07828d9d967c1c428821b1a7f8ccc072f361e1";
            AppUser user = userManager.FindByEmailAsync(email).Result;
            if (user == null)
            {
                var test = new AppUser()
                {
                    Email = email,
                    UserName = email,
                    Firstname = "Test",
                    Lastname = "Student"
                };

                var add = userManager.CreateAsync(test, root123SlashHash).Result;

                if (add != null)
                {
                    var addRole = userManager.AddToRoleAsync(test, "Student").Result;
                }
            }
        }
    }
}
