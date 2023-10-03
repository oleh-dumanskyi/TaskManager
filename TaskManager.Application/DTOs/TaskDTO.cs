using TaskManager.Domain.Entities;

namespace TaskManager.Application.DTOs
{
    public class TaskDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Status Status { get; set; }
        public List<Label> Labels { get; set; }
        public Guid AuthorId { get; set; }
        public Guid ExecutorId { get; set; }
    }
}
