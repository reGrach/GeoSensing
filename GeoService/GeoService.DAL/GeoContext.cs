using GeoService.DAL.Enums;
using Microsoft.EntityFrameworkCore;

namespace GeoService.DAL
{
    public class GeoContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Coordinate> Coordinates { get; set; }
        public DbSet<Polygon> Polygons { get; set; }

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

            builder.Entity<Team>()
                .HasOne(e => e.Polygon).WithOne(e => e.Team)
                .HasForeignKey<Polygon>(e => e.Id);

            builder.Entity<Team>().ToTable("Teams");

            builder.Entity<Polygon>().ToTable("Teams");

            builder.HasPostgresEnum<RoleEnum>();

            builder.HasPostgresEnum<ModeEnum>();
        }
    }
}
