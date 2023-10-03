using Microsoft.EntityFrameworkCore;
using ServerApp.Models.Local;

namespace ServerApp.Data
{
    public class ApiDbContext : DbContext
    {
        protected readonly IConfiguration _configuration;
        public ApiDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder option)
        {
            option.UseNpgsql(_configuration.GetConnectionString("DefaultConnection"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().HasIndex(u => u.Login).IsUnique();
        }

        public DbSet<User> User { get; set; }
        public DbSet<Department> Department { get; set; }
        public DbSet<Employee> Employee { get; set; }

        public DbSet<DSE> DSE { get; set; }
        public DbSet<Project> Project { get; set; }
        public DbSet<ProjectDocs> ProjectDocs { get; set; }
        public DbSet<ProjectStep> ProjectStep { get; set; }
        public DbSet<RNO> RNO { get; set; }
        public DbSet<Technology> Technology { get; set; }
    }
}
