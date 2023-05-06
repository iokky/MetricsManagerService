using MetricsManagerService.Models;
using Microsoft.EntityFrameworkCore;

namespace MetricsManagerService;

public class ServiceDbContext: DbContext
{
    public DbSet<CpuMetrics> CpuMetrics { get; set; }
    public DbSet<HddMetrics> HddMetrics { get; set; }
    public DbSet<NetworkMetrics> NetworkMetrics { get; set; }
    public DbSet<RamMetrics> RamMetrics { get; set; }
    public ServiceDbContext(DbContextOptions<ServiceDbContext>  options): base(options)
    {
        
    }
}
