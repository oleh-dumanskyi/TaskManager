using TaskManager.Domain.Entities;

namespace TaskManager.Application.Interfaces
{
    public interface IStatusService
    {
        Task<Guid> CreateStatus(string name, CancellationToken token);
        Task<Status> GetStatus(Guid id, CancellationToken token);
        Task<List<Status>> GetStatusList(CancellationToken token);
        Task<Guid> EditStatus(Guid id, string name, CancellationToken token);
        Task<Guid> DeleteStatus(Guid id, CancellationToken token);
    }
}
