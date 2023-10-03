using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using TaskManager.Application.Interfaces;

namespace TaskManager.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration["ConnectionString"];
            services.AddDbContext<TaskManagerDbContext>(option =>
            {
                option.UseSqlServer(connectionString);
            });

            services.AddScoped<ITaskManagerDbContext>(provider => provider
                .GetService<TaskManagerDbContext>());

            return services;
        }
    }
}
