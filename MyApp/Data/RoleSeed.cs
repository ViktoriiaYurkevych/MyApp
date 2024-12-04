using Microsoft.AspNetCore.Identity;
using MyApp.Views.Authentication;
using System.Threading.Tasks;

namespace MyApp.Data
{
    public static class RoleSeed
    {
        public static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
            {
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            }
            if (!await roleManager.RoleExistsAsync(UserRoles.Publisher))
            {
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Publisher));
            }
            if (!await roleManager.RoleExistsAsync(UserRoles.Author))
            {
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Author));
            }
            if (!await roleManager.RoleExistsAsync(UserRoles.User))
            {
                await roleManager.CreateAsync(new IdentityRole(UserRoles.User));
            }
        }
    }
}
