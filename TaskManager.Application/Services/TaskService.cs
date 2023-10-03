using Microsoft.EntityFrameworkCore;
using TaskManager.Application.DTOs;
using TaskManager.Application.Exceptions;
using TaskManager.Application.Interfaces;
using Task = TaskManager.Domain.Entities.Task;

namespace TaskManager.Application.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskManagerDbContext _context;

        public TaskService(ITaskManagerDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> CreateTask(TaskDTO request, CancellationToken token)
        {
            if (!await _context.Users.ContainsAsync(
                    await _context.Users.FirstOrDefaultAsync(i=>i.Id == request.AuthorId, token), token)
                || !await _context.Users.ContainsAsync(
                    await _context.Users.FirstOrDefaultAsync(i => i.Id == request.ExecutorId, token), token))
            {
                throw new NotFoundException(nameof(Task), request.ExecutorId + "/" + request.AuthorId);
            }

            if (!request.Labels.Any(_context.Labels.Contains))
            {
                throw new NotFoundException(nameof(Task), request.Labels[0].Id);
            }

            if (!await _context.Statuses.ContainsAsync(request.Status, token))
            {
                throw new NotFoundException(nameof(Task), request.Labels[0].Id);
            }

            var status = await _context.Statuses.FirstOrDefaultAsync(t => t.Id == request.Status.Id);
            var requestLabelsId = request.Labels.Select(t => t.Id).ToList();
            var labels = await _context.Labels.Where(i => requestLabelsId.Contains(i.Id)).ToListAsync();

            var task = new TaskManager.Domain.Entities.Task
            {
                Id = Guid.NewGuid(),
                Created = DateTime.Now,
                AuthorId = request.AuthorId,
                Description = request.Description,
                ExecutorId = request.ExecutorId,
                Labels = labels,
                Name = request.Name,
                Status = status
            };

            await _context.Tasks.AddAsync(task, token);
            await _context.SaveChangesAsync(token);
            return task.Id;
        }

        public async Task<Guid> EditTask(Guid id, TaskDTO request, CancellationToken token)
        {
            var task = await _context.Tasks
                .Include(i=>i.Labels)
                .Include(i=>i.Status)
                .FirstOrDefaultAsync(i => i.Id == id, token);
            if (task == null)
                throw new NotFoundException(nameof(Task), id);
            if (!(await _context.Users.ContainsAsync(
                    await _context.Users.FirstOrDefaultAsync(i => i.Id == request.AuthorId, token), token))
                || !await _context.Users.ContainsAsync(
                    await _context.Users.FirstOrDefaultAsync(i => i.Id == request.ExecutorId, token), token))
            {
                throw new NotFoundException(nameof(Task), request.ExecutorId + "/" + request.AuthorId);
            }
            if (!request.Labels.Any(_context.Labels.Contains))
            {
                throw new NotFoundException(nameof(Task), request.Labels[0].Id);
            }

            if (!await _context.Statuses.ContainsAsync(request.Status, token))
            {
                throw new NotFoundException(nameof(Task), request.Labels[0].Id);
            }

            var status = await _context.Statuses.FirstOrDefaultAsync(t => t.Id == request.Status.Id);
            var requestLabelsId = request.Labels.Select(t => t.Id).ToList();
            var labels = await _context.Labels.Where(i => requestLabelsId.Contains(i.Id)).ToListAsync();

            task.AuthorId = request.AuthorId;
            task.Description = request.Description;
            task.ExecutorId = request.ExecutorId;
            task.Labels = labels;
            task.Name = request.Name;
            task.Status = status;

            _context.Tasks.Update(task);
            await _context.SaveChangesAsync(token);
            return task.Id;
        }

        public async Task<Task> GetTask(Guid id, CancellationToken token)
        {
            var task = 
                await _context.Tasks
                    .Include(i=>i.Labels)
                    .Include(i=>i.Status)
                    .FirstOrDefaultAsync(i => i.Id == id, token);
            if(task == null)
                throw new NotFoundException(nameof(Task), id);
            return task;
        }

        public async Task<List<Task>> GetTaskList(CancellationToken token)
        {
            var tasks = await _context.Tasks
                .Include(i=>i.Labels)
                .Include(i=>i.Status)
                .ToListAsync(token);
            return tasks;
        }

        public async Task<Guid> DeleteTask(Guid id, CancellationToken token)
        {
            var task = await _context.Tasks.FirstOrDefaultAsync(i => i.Id == id, token);
            if (task == null)
                throw new NotFoundException(nameof(Task), id);
            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync(token);
            return task.Id;
        }
    }
}
