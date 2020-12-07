using Microsoft.EntityFrameworkCore;
using ModbusMaster.Client.Domain.Entities;

namespace ModbusMaster.Client.DAL
{
    public class ModbusClientContext : DbContext
    {
        public DbSet<ChannelConfig> Channels { get; set; }
        public DbSet<DeviceConfig> Devices { get; set; }
        public DbSet<RegisterConfig> Registers { get; set; }
        public DbSet<DumpConfig> Dumps { get; set; }

        public ModbusClientContext(DbContextOptions<ModbusClientContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ChannelConfig>(channel =>
            {
                channel.HasIndex(c => new { c.Title }).IsUnique();
            });

            modelBuilder.Entity<DeviceConfig>(device =>
            {
                device.HasIndex(d => new { d.Title }).IsUnique();
            });

            modelBuilder.Entity<RegisterConfig>(register =>
            {
                register.HasIndex(r => new { r.Title }).IsUnique();
            });
        }
    }
}
