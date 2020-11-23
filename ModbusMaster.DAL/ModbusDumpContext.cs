using Microsoft.EntityFrameworkCore;
using ModbusMaster.Domain.Entities;

namespace ModbusMaster.DAL
{
    public class ModbusDumpContext : DbContext
    {
        public DbSet<Channel> Channels { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<Register> Registers { get; set; }
        public DbSet<Dump> Dumps { get; set; }

        public ModbusDumpContext(DbContextOptions options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Seed();
        }
    }
}
