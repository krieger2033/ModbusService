using ModbusMaster.Domain.Entities;
using ModbusMaster.PollAgent.DAL.Interfaces;

namespace ModbusMaster.PollAgent.DAL.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ModbusPollContext _context;

        public IChannelRepository ChannelsRepository => new ChannelRepository(_context);
        public IRepository<Device> DevicesRepository => new Repository<Device>(_context);
        public IRepository<Register> RegistersRepository => new Repository<Register>(_context);
        public IDumpRepository DumpsRepository => new DumpRepository(_context);

        public UnitOfWork(ModbusPollContext context)
        {
            _context = context;
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }
    }
}
