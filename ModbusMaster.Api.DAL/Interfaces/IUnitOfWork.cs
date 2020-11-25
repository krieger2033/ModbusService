﻿using ModbusMaster.Api.Domain.Entities;
using ModbusMaster.Domain.Entities;
//using ModbusMaster.Api.Domain.Entities;

namespace ModbusMaster.Api.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        //IRepository<Channel> ChannelsRepository { get; }
        //IRepository<Device> DevicesRepository { get; }
        //IRepository<Register> RegistersRepository { get; }
        //IRepository<Dump> DumpsRepository { get; }
        IRepository<AppUser> UsersRepository { get; }

        int SaveChanges();
    }
}