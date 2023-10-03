using Microsoft.EntityFrameworkCore;
using ServerApp.Models._1C_DB;

namespace ServerApp.Data
{
    public class _1cDbContext : DbContext
    {
        protected readonly IConfiguration _configuration;
        public _1cDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder option)
        {
            option.UseNpgsql(_configuration.GetConnectionString("1C-DB_Connection"));

        }
        public DbSet<ProjectAgregat> ProjectAgregat { get; set; }
        public DbSet<DSE> DSE { get; set; }
        public DbSet<Project> Project { get; set; }
        public DbSet<VydachaTrebov> VydachaTrebov { get; set; }
        public DbSet<RNO> RNO { get; set; }
    }
}