namespace TaskManager.Domain.Entities
{
    public class Task
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Status Status { get; set; }
        public Guid StatusId { get; set; }
        public List<Label> Labels { get; set; }
        public DateTime Created { get; set; }
        public Guid AuthorId { get; set; }
        public Guid ExecutorId { get; set; }
    }
}