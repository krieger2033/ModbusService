using ModbusMaster.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ModbusMaster.DAL.Interfaces
{
    public interface IChannelRepository: IRepository<Channel>
    {
        List<Channel> GetConfig();
    }
}
