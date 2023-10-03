using TaskManager.Domain.Entities;

namespace TaskManager.Application.Interfaces
{
    public interface ILabelService
    {
        Task<Guid> CreateLabel(string name, CancellationToken token);
        Task<Label> GetLabel(Guid id, CancellationToken token);
        Task<List<Label>> GetLabelList(CancellationToken token);
        Task<Guid> EditLabel(Guid id, string name, CancellationToken token);
        Task<Guid> DeleteLabel(Guid id, CancellationToken token);
    }
}
