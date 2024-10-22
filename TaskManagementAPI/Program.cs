using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Configuration;
using TaskInfrastructureLayer;
using TaskManagementDomain;

namespace TaskManagementAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connectionString = builder.Configuration.GetConnectionString("database");
            builder.Services.AddDbContext<TaskContext>(options =>
                    options.UseSqlServer(connectionString));

            builder.Services.AddScoped<ITaskDetailService, TaskDetailService>();

            builder.Services.AddControllers();

            var app = builder.Build();

            app.MapControllers();

            app.Run();
        }
    }
}
