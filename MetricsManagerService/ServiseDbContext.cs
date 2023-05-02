using MetricsManagerService.Models;
using Microsoft.EntityFrameworkCore;

namespace MetricsManagerService;

public class ServiseDbContext: DbContext
{
    public DbSet<Agent> Agents { get; set; }
    public ServiseDbContext(DbContextOptions<ServiseDbContext>  options): base(options)
    {
        
    }
}
