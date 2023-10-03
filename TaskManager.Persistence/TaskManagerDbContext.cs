using Microsoft.EntityFrameworkCore;
using TaskManager.Application.Interfaces;
using TaskManager.Persistence.EntityTypeConfiguration;
using TaskManager.Domain.Entities;
using Label = TaskManager.Domain.Entities.Label;

namespace TaskManager.Persistence
{
    public class TaskManagerDbContext : DbContext, ITaskManagerDbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Label> Labels { get; set; }
        public DbSet<Domain.Entities.Task> Tasks { get; set; }
        public TaskManagerDbContext(DbContextOptions<TaskManagerDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new LabelConfiguration());
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new TaskConfiguration());
            builder.ApplyConfiguration(new StatusConfiguration());

            base.OnModelCreating(builder);
        }
    }
}