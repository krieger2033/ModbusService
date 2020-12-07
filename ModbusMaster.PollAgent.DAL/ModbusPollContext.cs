using Microsoft.EntityFrameworkCore;
using ModbusMaster.Domain.Entities;

namespace ModbusMaster.PollAgent.DAL
{
    public class ModbusPollContext : DbContext
    {
        public DbSet<Channel> Channels { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<Register> Registers { get; set; }
        public DbSet<Dump> Dumps { get; set; }

        public ModbusPollContext(DbContextOptions options) : base(options)
        {
        }
    }
}
