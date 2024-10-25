using Microsoft.EntityFrameworkCore;

namespace TaskInfrastructureLayer
{
    public class TaskContext : DbContext
    {
        public TaskContext()
        {
        }
        public TaskContext(DbContextOptions<TaskContext> options) : base(options) { }

        public virtual DbSet<TaskDetail> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaskDetail>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("Id")
                    .ValueGeneratedOnAdd();
            });

        }
    }

}
