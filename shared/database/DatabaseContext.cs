using Microsoft.EntityFrameworkCore;

namespace warren_analysis_desk;

public class DatabaseContext : DbContext
{
    public DbSet<News> News { get; set; }
    public DbSet<RobotKeys> RobotKeys { get; set; }
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) {}
}