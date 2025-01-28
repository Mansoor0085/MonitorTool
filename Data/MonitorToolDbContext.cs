using Microsoft.EntityFrameworkCore;
using MonitorTool.Models;

namespace MonitorTool.Data
{
    public class MonitorToolDbContext : DbContext
    {
        public MonitorToolDbContext(DbContextOptions<MonitorToolDbContext> options) : base(options)
        {
            
        }
       public DbSet<Student> Students { get; set; }       
       public DbSet<Login> Login { get; set; }
       public DbSet<MCQuestions> Questions { get; set; }
       public DbSet<MCOptions> Options { get; set; }

    }
}
