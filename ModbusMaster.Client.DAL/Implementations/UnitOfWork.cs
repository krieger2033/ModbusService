using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ModbusMaster.Client.DAL.Interfaces;
using ModbusMaster.Client.Domain.Entities;
using ModbusMaster.DAL;
using ModbusMaster.Domain.Entities;

namespace ModbusMaster.Client.DAL.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ModbusClientContext _context;

        public IChannelRepository ChannelsRepository => new ChannelRepository(_context);
        public IRepository<DeviceConfig> DevicesRepository => new Repository<DeviceConfig>(_context);
        public IRepository<RegisterConfig> RegistersRepository => new Repository<RegisterConfig>(_context);
        public IRepository<DumpConfig> DumpsRepository => new Repository<DumpConfig>(_context);

        public UnitOfWork(ModbusClientContext context)
        {
            _context = context;
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
