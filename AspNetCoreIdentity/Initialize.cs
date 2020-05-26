using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreIdentity
{
    public class Initialize
    {
        public async static void InitializeData(IServiceProvider provider)
        {
            using (var scope = provider.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                bool isAdminExist = await roleManager.RoleExistsAsync("Admin");

                if (!isAdminExist)
                {
                    var identityRole = new IdentityRole();
                    identityRole.Name = "Admin";
                    var result = await roleManager.CreateAsync(identityRole);
                }

                bool isClientExist = await roleManager.RoleExistsAsync("Client");

                if (!isClientExist)
                {
                    var identityRole = new IdentityRole();
                    identityRole.Name = "Client";
                    var result = await roleManager.CreateAsync(identityRole);
                }
            }
        }

    }
}
