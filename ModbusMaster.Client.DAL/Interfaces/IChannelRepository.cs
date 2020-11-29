using System.Collections.Generic;
using System.Threading.Tasks;
using ModbusMaster.Client.Domain.Entities;

namespace ModbusMaster.Client.DAL.Interfaces
{
    public interface IChannelRepository: IRepository<ChannelConfig>
    {
        Task<List<ChannelConfig>> GetConfig(string userId);
    }
}
