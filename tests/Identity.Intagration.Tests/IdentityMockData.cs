using Identity.Infra.Repositories.Context;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Intagration.Tests
{
    public class IdentityMockData
    {
        public static async Task CreateCategories(IdentityApiApplication application, bool criar)
        {
            using (var scope = application.Services.CreateScope())
            {
                var provider = scope.ServiceProvider;
                using (var IdentityDbContext = provider.GetRequiredService<ApplicationDbContext>())
                {
                    await IdentityDbContext.Database.EnsureCreatedAsync();

                    if (criar)
                    {
                        await IdentityDbContext.Users.AddAsync(new ApplicationUser
                        { FullName = "Categoria 1", Email = "Descricao Categoria 1" });

                        await IdentityDbContext.SaveChangesAsync();
                    }
                }
            }
        }
    }
}