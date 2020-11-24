﻿using ModbusMaster.DAL.Interfaces;
using ModbusMaster.Domain.Entities;

namespace ModbusMaster.DAL.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ModbusDumpContext _context;

        public IChannelRepository ChannelsRepository => new ChannelRepository(_context);
        public IRepository<Device> DevicesRepository => new Repository<Device>(_context);
        public IRepository<Register> RegistersRepository => new Repository<Register>(_context);
        public IDumpRepository DumpsRepository => new DumpRepository(_context);

        public UnitOfWork(ModbusDumpContext context)
        {
            _context = context;
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }
    }
}
