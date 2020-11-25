using Microsoft.EntityFrameworkCore;
using ModbusMaster.Client.Domain.Entities;
using ModbusMaster.Domain.Entities;

namespace ModbusMaster.DAL
{
    public class ApiDataContext : DbContext
    {
        //public DbSet<Channel> Channels { get; set; }
        //public DbSet<Device> Devices { get; set; }
        //public DbSet<Register> Registers { get; set; }
        //public DbSet<Dump> Dumps { get; set; }
        public DbSet<AppUser> Users { get; set; }

        public ApiDataContext(DbContextOptions options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.SeedApi();
        }
    }
}
