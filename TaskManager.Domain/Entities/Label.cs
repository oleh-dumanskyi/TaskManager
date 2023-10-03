using System.Text.Json.Serialization;

namespace TaskManager.Domain.Entities
{
    public class Label
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime Created { get; set; }
        [JsonIgnore]
        public List<Task>? Tasks { get; set; }
    }
}
