using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Identity.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
                    new IdentityRole
                    {
                        Id = "2f226492-f709-4c4c-afe8-2715c95186f6",
                        Name = "Administrador",
                        NormalizedName = "Administrador"
                        
                    },
                     new IdentityRole
                     {
                         Id = "98c84c8d-8dd9-4da8-9528-92e48ead6df0",
                         Name = "Operador",
                         NormalizedName = "Operador"

                     }
                );
        }
    }
}
