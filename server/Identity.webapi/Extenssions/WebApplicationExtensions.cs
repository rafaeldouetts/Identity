using Microsoft.EntityFrameworkCore;

namespace Identity.webapi.Extenssions
{
    public static class WebApplicationExtensions
    {
        public static void ApplyMigrations<TContext>(this WebApplication app) where TContext : DbContext
        {
            using var scope = app.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<TContext>();
            dbContext.Database.Migrate(); // Executa as migrations pendentes
        }
    }
}
