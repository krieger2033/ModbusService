using ModbusMaster.Client.DAL.Interfaces;
using ModbusMaster.Client.Domain.Entities;
using ModbusMaster.DAL;

namespace ModbusMaster.Client.DAL.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApiDataContext _context;

        //public IRepository<Channel> ChannelsRepository => new Repository<Channel>(_context);
        //public IRepository<Device> DevicesRepository => new Repository<Device>(_context);
        //public IRepository<Register> RegistersRepository => new Repository<Register>(_context);
        //public IRepository<Dump> DumpsRepository => new Repository<Dump>(_context);
        public IRepository<AppUser> UsersRepository => new Repository<AppUser>(_context);

        public UnitOfWork(ApiDataContext context)
        {
            _context = context;
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }
    }
}
