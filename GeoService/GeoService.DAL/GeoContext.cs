using GeoService.DAL.Enums;
using Microsoft.EntityFrameworkCore;

namespace GeoService.DAL
{
    public class GeoContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Experiment> Experiments { get; set; }
        public DbSet<GeoParameter> GeoParameters { get; set; }

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

            //Создаем ForeginKey без navigation property 
            builder.Entity<Experiment>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(p => p.CreatorUserId);

            builder.HasPostgresEnum<RoleEnum>();

            builder.HasPostgresEnum<ModeEnum>();
        }
    }
}
