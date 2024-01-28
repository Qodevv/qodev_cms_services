using Microsoft.EntityFrameworkCore;
using qodev_content_management_services.models;

namespace qodev_content_management_services.db;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options){}
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
    public DbSet<Cms> CmsEnumerable { get; set; }
}