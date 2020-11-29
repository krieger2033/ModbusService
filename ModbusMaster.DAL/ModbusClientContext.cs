using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ModbusMaster.Client.Domain.Entities;
using ModbusMaster.Domain.Entities;

namespace ModbusMaster.DAL
{
    public class ModbusClientContext : IdentityDbContext<ApplicationUser, ApplicationRole, string, IdentityUserClaim<string>,
        ApplicationUserRole, IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>>
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

            modelBuilder.Entity<ApplicationUserRole>(userRole =>
            {
                userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

                userRole.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();

                userRole.HasOne(ur => ur.User)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });

            modelBuilder.Entity<ChannelConfig>(channel =>
            {
                channel.HasIndex(c => new { c.Title, c.UserId }).IsUnique();

                channel.HasOne(c => c.User)
                    .WithMany(u => u.Channels)
                    .HasForeignKey(c => c.UserId)
                    .IsRequired();
            });

            modelBuilder.Entity<DeviceConfig>(channel =>
            {
                channel.HasIndex(c => new { c.Title }).IsUnique();
            });

            modelBuilder.Entity<RegisterConfig>(channel =>
            {
                channel.HasIndex(c => new { c.Title }).IsUnique();
            });
        }
    }
}
