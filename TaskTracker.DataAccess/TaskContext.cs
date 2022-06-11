using Microsoft.EntityFrameworkCore;

namespace TaskTracker.DataAccess
{
    public class TaskContext : DbContext
    {
        public DbSet<WorkTask> Tasks { get; set; }
        public TaskContext(DbContextOptions options) : base(options)
        {

        }

    }
}