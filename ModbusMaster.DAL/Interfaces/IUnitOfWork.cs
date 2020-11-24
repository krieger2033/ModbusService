﻿using ModbusMaster.Domain.Entities;

namespace ModbusMaster.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IChannelRepository ChannelsRepository { get; }
        IRepository<Device> DevicesRepository { get; }
        IRepository<Register> RegistersRepository { get; }
        IDumpRepository DumpsRepository { get; }
        int SaveChanges();
    }
}
