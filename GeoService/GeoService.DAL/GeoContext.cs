using GeoService.DAL.Enums;
using Microsoft.EntityFrameworkCore;

namespace GeoService.DAL
{
    public class GeoContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Coordinate> Coordinates { get; set; }

        public GeoContext(DbContextOptions<GeoContext> options) : base(options)
            => Database.EnsureCreated();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>()
                .HasIndex(u => u.Login)
                .IsUnique();

            builder.Entity<Team>()
                .HasIndex(u => u.Title)
                .IsUnique();

            builder.Entity<Team>()
                .HasIndex(u => u.Color)
                .IsUnique();

            builder.HasPostgresEnum<RoleEnum>();

            builder.HasPostgresEnum<ModeEnum>();
        }
    }
}
