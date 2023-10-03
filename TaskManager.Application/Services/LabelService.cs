using Microsoft.EntityFrameworkCore;
using TaskManager.Application.Exceptions;
using TaskManager.Application.Interfaces;
using TaskManager.Domain.Entities;

namespace TaskManager.Application.Services
{
    public class LabelService : ILabelService
    {
        private readonly ITaskManagerDbContext _context;

        public LabelService(ITaskManagerDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> CreateLabel(string name, CancellationToken token)
        {
            if(name == null)
                throw new ArgumentNullException();

            var label = new Label
            {
                Id = Guid.NewGuid(),
                Name = name,
                Created = DateTime.Now
            };

            await _context.Labels.AddAsync(label, token);
            await _context.SaveChangesAsync(token);

            return label.Id;
        }

        public async Task<Label> GetLabel(Guid id, CancellationToken token)
        {
            var label = await _context.Labels.FirstOrDefaultAsync(x => x.Id == id, token);
            if (label == null)
                throw new NotFoundException(nameof(Label), id);
            return label;
        }

        public async Task<List<Label>> GetLabelList(CancellationToken token)
        {
            var labels = await _context.Labels.ToListAsync(token);
            return labels;
        }

        public async Task<Guid> EditLabel(Guid id, string name ,CancellationToken token)
        {
            var label = await _context.Labels.FirstOrDefaultAsync(i => i.Id == id);
            if (label == null)
                throw new NotFoundException(nameof(Label), id);
            label.Name = name;
            _context.Labels.Update(label);
            await _context.SaveChangesAsync(token);
            return label.Id;
        }

        public async Task<Guid> DeleteLabel(Guid id, CancellationToken token)
        {
            var label = await _context.Labels.FirstOrDefaultAsync(i => i.Id == id);
            if (label == null)
                throw new NotFoundException(nameof(Label), id);
            _context.Labels.Remove(label);
            await _context.SaveChangesAsync(token);
            return label.Id;
        }
    }
}
