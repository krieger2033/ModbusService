using System.Collections.Generic;
using ModbusMaster.Domain.Entities;

namespace ModbusMaster.PollAgent.DAL.Interfaces
{
    public interface IChannelRepository: IRepository<Channel>
    {
        List<Channel> GetConfig();
    }
}
