using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManager.Domain.Entities;

namespace TaskManager.Persistence.EntityTypeConfiguration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Email).IsRequired();
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Surname).IsRequired();
            builder.Property(x => x.Password).IsRequired();
        }
    }
}
