using Microsoft.EntityFrameworkCore;
using TaskManager.Application.Exceptions;
using TaskManager.Application.Interfaces;
using TaskManager.Domain.Entities;

namespace TaskManager.Application.Services
{
    public class StatusService : IStatusService
    {
        private readonly ITaskManagerDbContext _context;

        public StatusService(ITaskManagerDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> CreateStatus(string name, CancellationToken token)
        {
            var status = new Status
            {
                Id = Guid.NewGuid(),
                Name = name,
                Created = DateTime.Now
            };

            await _context.Statuses.AddAsync(status, token);
            await _context.SaveChangesAsync(token);
            return status.Id;
        }

        public async Task<Status> GetStatus(Guid id, CancellationToken token)
        {
            var status = await _context.Statuses.FirstOrDefaultAsync(i => i.Id == id, token);
            if (status == null)
                throw new NotFoundException(nameof(Status), id);
            return status;
        }

        public async Task<List<Status>> GetStatusList(CancellationToken token)
        {
            var statuses = await _context.Statuses.ToListAsync(token);
            return statuses;
        }

        public async Task<Guid> EditStatus(Guid id, string name, CancellationToken token)
        {
            var status = await _context.Statuses.FirstOrDefaultAsync(i => i.Id == id, token);
            if(status == null)
                throw new NotFoundException(nameof(Status), id);
            status.Name = name;
            _context.Statuses.Update(status);
            await _context.SaveChangesAsync(token);
            return status.Id;
        }

        public async Task<Guid> DeleteStatus(Guid id, CancellationToken token)
        {
            var status = await _context.Statuses.FirstOrDefaultAsync(i => i.Id == id, token);
            if (status == null)
                throw new NotFoundException(nameof(Status), id);
            _context.Statuses.Remove(status);
            await _context.SaveChangesAsync(token);
            return status.Id;
        }
    }
}
