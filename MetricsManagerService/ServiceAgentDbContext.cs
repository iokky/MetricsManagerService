using MetricsManagerService.Models;
using Microsoft.EntityFrameworkCore;

namespace MetricsManagerService
{
    public class ServiceAgentDbContext: DbContext
    {
        public DbSet<Agent> Agents { get; set; }
        public ServiceAgentDbContext(DbContextOptions<ServiceAgentDbContext> options):base(options) 
        {
            
        }
    }
}
