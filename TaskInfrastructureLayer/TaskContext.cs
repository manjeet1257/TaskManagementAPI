using Microsoft.EntityFrameworkCore;

namespace TaskInfrastructureLayer
{
    public class TaskContext : DbContext
    {
        public TaskContext(DbContextOptions<TaskContext> options) : base(options) { }

        public DbSet<TaskDetail> Tasks { get; set; }
    }

}
