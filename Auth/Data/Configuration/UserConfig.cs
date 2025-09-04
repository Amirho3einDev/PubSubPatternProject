using Auth.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Auth.Data.Configuration
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(x => x.Username).IsRequired();
            builder.Property(x => x.Password).IsRequired();
            builder.Property(x => x.Role).HasMaxLength(50).IsRequired();
            builder.HasData(new List<User>()
            {
                //TODO: Password Should Be Hash
                new User()
                {
                    Id=Guid.NewGuid(),
                    Username="Manager",
                    Password="1234",
                    Role="Admin"
                },
                new User()
                {
                    Id=Guid.NewGuid(),
                    Username="user1",
                    Password="1234",
                    Role="employee"
                }, 
            });
        }
    }
}
