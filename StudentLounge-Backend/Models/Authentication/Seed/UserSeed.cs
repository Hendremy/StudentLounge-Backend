using Microsoft.AspNetCore.Identity;

namespace StudentLounge_Backend.Models.Authentication.Seed
{
    public class UserSeed
    {
        public static void AddDefaultRoles(RoleManager<IdentityRole> roleManager)
        {
            IdentityRole role = roleManager.FindByIdAsync("1").Result;
            if (role == null)
            {
                var add = roleManager.CreateAsync(new IdentityRole("Admin")).Result;
                add = roleManager.CreateAsync(new IdentityRole("Student")).Result;
            }
        }

        public static void AddDefaultUser(UserManager<AppUser> userManager)
        {
            AppUser user = userManager.FindByIdAsync("1").Result;
            if (user == null)
            {
                var admin = new AppUser()
                {
                    Email = "admin@studentlounge.com",
                    UserName = "admin@studentlounge.com",
                    Firstname = "Admin",
                    Lastname = "Admin"
                };

                var add = userManager.CreateAsync(admin, "Root123/").Result;

                if (add != null)
                {
                    var addRole = userManager.AddToRoleAsync(admin, "Admin").Result;
                }
            }

        }
    }
}
