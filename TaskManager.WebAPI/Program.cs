using Microsoft.AspNetCore.Authentication.Cookies;
using TaskManager.Application;
using TaskManager.Persistence;

namespace TaskManager.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var configuration = builder.Configuration;
            builder.Services.AddPersistence(configuration);
            using (var scope = builder.Services.BuildServiceProvider().CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;
                try
                {
                    var context = serviceProvider.GetRequiredService<TaskManagerDbContext>();
                    DbInitializer.Initialize(context);
                }
                catch (Exception e)
                {
                    
                }
            }
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddApplication();
            builder.Services.AddAuthorization();
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(opt =>
                {
                    opt.LoginPath = "/User/SignIn";
                    opt.LogoutPath = "/User/SignOut";
                    opt.ExpireTimeSpan = TimeSpan.FromMinutes(15);
                });

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin();
                    policy.AllowAnyMethod();
                    policy.AllowAnyHeader();
                });
            });

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors("AllowAll");

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}