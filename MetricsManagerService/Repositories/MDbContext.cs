using Microsoft.EntityFrameworkCore;
using MetricsManagerService.Models;

namespace MetricsManagerService.Repositories;

public class MDbContext: DbContext
{
    public MDbContext(DbContextOptions<MDbContext> options): base(options)
    {
        
    }

    public DbSet<ShortLinkModel> ShortLink { get; set; }
}
