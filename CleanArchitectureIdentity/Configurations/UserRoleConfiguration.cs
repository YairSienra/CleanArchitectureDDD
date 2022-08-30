using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Identity.Configurations
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            builder.HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "2f226492-f709-4c4c-afe8-2715c95186f6",
                    UserId = "a31d3eb8-83b1-4887-adb5-aacd4464e90b"
                },
                new IdentityUserRole<string>
                {
                    RoleId = "98c84c8d-8dd9-4da8-9528-92e48ead6df0",
                    UserId = "bbcafa07-38b8-4b0f-aa19-2284b471256c"
                }

                );
        }
    }
}
