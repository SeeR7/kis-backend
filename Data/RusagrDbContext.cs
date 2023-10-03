using Microsoft.EntityFrameworkCore;
using ServerApp.Models.Rusagr;

namespace ServerApp.Data
{
    public class RusagrDbContext : DbContext
    {
        protected readonly IConfiguration _configuration;
        public RusagrDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder option)
        {
            option.UseNpgsql(_configuration.GetConnectionString("RUSAGR-DB_Connection"));

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<DSE>().HasIndex(u => u.DseCode).IsUnique();
        }

        public DbSet<DepRoute> DepRoute { get; set; }
        public DbSet<DSE> DSE { get; set; }
        public DbSet<DseSostav> DseSostav { get; set; }
    }
}
