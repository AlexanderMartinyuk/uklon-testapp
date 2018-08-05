using Microsoft.EntityFrameworkCore;
using WebAPI.Models;

namespace WebAPI.Services.Implementation
{
    public class CacheStorageContext : DbContext
    {
        private readonly string DataSourceConnection = "Data Source=cache.db";

        public DbSet<TrafficModel> Traffics { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(DataSourceConnection);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<TrafficModel>().HasKey(t => t.RegionCode);
        }
    }
}