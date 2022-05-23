using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WellDoneProjectAngular.Core.Costants;
using WellDoneProjectAngular.Core.Models;

namespace WellDoneProjectAngular.Infrastructure
{
    public static class AppDbContextSeeder
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            if (!await roleManager.RoleExistsAsync(AuthorizationConstants.Roles.Admin))
            {
                await roleManager.CreateAsync(new IdentityRole(AuthorizationConstants.Roles.Admin));
            }

            if (!await roleManager.RoleExistsAsync(AuthorizationConstants.Roles.User))
            {
                await roleManager.CreateAsync(new IdentityRole(AuthorizationConstants.Roles.User));
            }

            var adminUser = new ApplicationUser
            {
                UserName = "admin",
                Email = "admin@microsoft.com",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString(),
            };

            var userExists = await userManager.FindByNameAsync(adminUser.UserName);
            if (userExists == null)
            {
                var result = await userManager.CreateAsync(adminUser, AuthorizationConstants.DefaultPassword);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, AuthorizationConstants.Roles.Admin);
                }
            }

            var standardUser = new ApplicationUser
            {
                UserName = "user",
                Email = "demouser@microsoft.com",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString(),
            };

            var standardUserExists = await userManager.FindByNameAsync(standardUser.UserName);
            if (standardUserExists == null)
            {
                var result = await userManager.CreateAsync(standardUser, AuthorizationConstants.DefaultPassword);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(standardUser, AuthorizationConstants.Roles.User);
                }
            }

            var robotUser = new ApplicationUser
            {
                UserName = "Daemon",
                Email = "daemon@microsoft.com",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString(),
            };

            var robotExists = await userManager.FindByNameAsync(robotUser.UserName);
            if (robotExists == null)
            {
                var result = await userManager.CreateAsync(robotUser, AuthorizationConstants.DefaultPassword);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(robotUser, AuthorizationConstants.Roles.User);
                    await userManager.AddToRoleAsync(robotUser, AuthorizationConstants.Roles.Admin);
                }
            }
        }


        public static async Task SeedAsyn2c(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            await roleManager.CreateAsync(new IdentityRole(AuthorizationConstants.Roles.Admin));

            var defaultUser = new ApplicationUser { UserName = "demouser@microsoft.com", Email = "demouser@microsoft.com" };
            await userManager.CreateAsync(defaultUser, AuthorizationConstants.DefaultPassword);

            string adminUserName = "admin@microsoft.com";
            var adminUser = new ApplicationUser { UserName = adminUserName, Email = adminUserName };
            await userManager.CreateAsync(adminUser, AuthorizationConstants.DefaultPassword);
            adminUser = await userManager.FindByNameAsync(adminUserName);
            await userManager.AddToRoleAsync(adminUser, AuthorizationConstants.Roles.Admin);
        }

    }
}
