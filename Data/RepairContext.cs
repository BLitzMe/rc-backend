using Microsoft.EntityFrameworkCore;
using RepairConsole.Data.Models;

namespace RepairConsole.Data
{
    public class RepairContext : DbContext
    {
        public DbSet<UserDevice> UserDevices { get; set; }
        public DbSet<RepairDevice> RepairDevices { get; set; }
        public DbSet<RepairDocument> RepairDocuments { get; set; }

        public RepairContext(DbContextOptions<RepairContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserDevice>()
                .HasOne(u => u.RepairDevice)
                .WithMany(r => r.UserDevices)
                .HasForeignKey(u => u.RepairDeviceId);

            modelBuilder.Entity<RepairDocument>()
                .HasOne(d => d.RepairDevice)
                .WithMany(r => r.Documents)
                .HasForeignKey(d => d.RepairDeviceId);

            base.OnModelCreating(modelBuilder);
        }
    }
}