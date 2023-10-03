using Microsoft.Extensions.DependencyInjection;
using TaskManager.Application.Interfaces;
using TaskManager.Application.Services;

namespace TaskManager.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddTransient<ILabelService, LabelService>();
            services.AddTransient<IStatusService, StatusService>();
            services.AddTransient<ITaskService, TaskService>();
            services.AddTransient<IUserService, UserService>();
            return services;
        }
    }
}
