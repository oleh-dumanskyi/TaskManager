using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Task = TaskManager.Domain.Entities.Task;

namespace TaskManager.Persistence.EntityTypeConfiguration
{
    public class TaskConfiguration : IEntityTypeConfiguration<Task>
    {
        public void Configure(EntityTypeBuilder<Task> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Description).IsRequired();
            builder.Property(x => x.Created).IsRequired();
            builder.HasOne(t => t.Status).WithMany(t => t.Tasks);
            builder.HasMany(t => t.Labels).WithMany(t => t.Tasks);
        }
    }
}
