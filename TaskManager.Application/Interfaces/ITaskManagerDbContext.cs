using Microsoft.EntityFrameworkCore;
using TaskManager.Domain.Entities;
using Task = TaskManager.Domain.Entities.Task;

namespace TaskManager.Application.Interfaces
{
    public interface ITaskManagerDbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Label> Labels { get; set; }
        public DbSet<Task> Tasks { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
