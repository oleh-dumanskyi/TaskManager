using TaskManager.Application.DTOs;

namespace TaskManager.Application.Interfaces
{
    public interface ITaskService
    {
        Task<Guid> CreateTask(TaskDTO request, CancellationToken token);
        Task<Guid> EditTask(Guid id, TaskDTO request, CancellationToken token);
        Task<Domain.Entities.Task> GetTask(Guid id, CancellationToken token);
        Task<List<Domain.Entities.Task>> GetTaskList(CancellationToken token);
        Task<Guid> DeleteTask(Guid id, CancellationToken token);
    }
}
