using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Infra.Repositories.Context
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Configurar a propriedade ProfilePictureUrl
            builder.Entity<ApplicationUser>(entity =>
            {
                entity.Property(e => e.ProfilePictureUrl)
                      .HasMaxLength(255) // Limite de caracteres (opcional)
                      .IsRequired(false) // Permitir nulo, se necessário
                      .HasDefaultValue("https://example.com/default-profile-picture.png"); // Valor padrão
            });
        }

    }
}
