using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ModbusMaster.DAL
{
    public class ModbusIdentityContext : IdentityDbContext
    {
        public ModbusIdentityContext(DbContextOptions<ModbusIdentityContext> options)
            : base(options)
        {
        }
    }
}
