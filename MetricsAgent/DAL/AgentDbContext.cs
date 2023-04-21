using MetricsAgent.Models;
using Microsoft.EntityFrameworkCore;

namespace MetricsAgent.DAL;

public class AgentDbContext: DbContext
{
    public DbSet<CpuMetrics> cpuMetrics { get; set; }
    public DbSet<DotNetMetrics> dotNetMetrics { get; set; }

    public AgentDbContext(DbContextOptions<AgentDbContext> options): base(options)
    {
        
    }
}
