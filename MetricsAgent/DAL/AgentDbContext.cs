using MetricsAgent.Models;
using Microsoft.EntityFrameworkCore;

namespace MetricsAgent.DAL;

public class AgentDbContext: DbContext
{
    public DbSet<CpuMetrics> cpuMetrics { get; set; }
    public DbSet<DotNetMetrics> dotNetMetrics { get; set; }
    public DbSet<HddMetrics> hddMetrics { get; set; }
    public DbSet<NetworkMetrics> networkMetrics { get; set; }
    public DbSet<RamMetrics> ramMetrics { get; set; }

    public AgentDbContext(DbContextOptions<AgentDbContext> options): base(options)
    {
        
    }
}
