using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RepairConsole.Data.Models;

namespace RepairConsole.Data
{
    public class RepairContext : DbContext
    {
        public DbSet<UserDevice> UserDevices { get; set; }
        public DbSet<RepairDevice> RepairDevices { get; set; }
        public DbSet<RepairDocument> RepairDocuments { get; set; }
        public DbSet<Link> Links { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<LinkRating> LinkRatings { get; set; }
        public DbSet<DocumentRating> DocumentRatings { get; set; }
        public DbSet<WorkDuration> WorkDurations { get; set; }

        public RepairContext(DbContextOptions<RepairContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var durationConverter = new EnumToStringConverter<DurationType>();

            modelBuilder.Entity<UserDevice>()
                .HasOne(u => u.RepairDevice)
                .WithMany(r => r.UserDevices)
                .HasForeignKey(u => u.RepairDeviceId);

            modelBuilder.Entity<RepairDocument>()
                .HasOne(d => d.RepairDevice)
                .WithMany(r => r.Documents)
                .HasForeignKey(d => d.RepairDeviceId);

            modelBuilder.Entity<Link>()
                .HasOne(l => l.RepairDevice)
                .WithMany(dev => dev.Links)
                .HasForeignKey(l => l.RepairDeviceId);

            modelBuilder.Entity<LinkRating>()
                .HasOne(rating => rating.Link)
                .WithMany(link => link.Ratings)
                .HasForeignKey(rating => rating.LinkId);

            modelBuilder.Entity<WorkDuration>()
                .Property(w => w.Type)
                .HasConversion(durationConverter);

            modelBuilder.Entity<WorkDuration>()
                .HasOne(w => w.Device)
                .WithMany(ud => ud.Durations)
                .HasForeignKey(w => w.UserDeviceId);

            base.OnModelCreating(modelBuilder);
        }
    }
}