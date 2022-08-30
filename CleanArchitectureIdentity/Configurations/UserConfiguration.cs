using CleanArchitecture.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Identity.Configurations
{
    internal class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            var hasher = new PasswordHasher<ApplicationUser>();
            builder.HasData(
                    new ApplicationUser
                    {
                        Id = "a31d3eb8-83b1-4887-adb5-aacd4464e90b",
                        Email = "admin@localhost.com",
                        NormalizedEmail = "admin@localhost.com",
                        Nombre = "Yair",
                        Apellido = "Sienra",
                        UserName = "Yair75",
                        NormalizedUserName = "Yair75",
                        PasswordHash = hasher.HashPassword(null, "Lonely75$"),
                        EmailConfirmed = true
                    },
                    new ApplicationUser
                    {
                        Id = "bbcafa07-38b8-4b0f-aa19-2284b471256c",
                        Email = "user@localhost.com",
                        NormalizedEmail = "user@localhost.com",
                        Nombre = "Manuel",
                        Apellido = "Gutierrez",
                        UserName = "Guti75",
                        NormalizedUserName = "Guti75",
                        PasswordHash = hasher.HashPassword(null, "Lonely75$"),
                        EmailConfirmed = true
                    }
                );
        }
    }
}
