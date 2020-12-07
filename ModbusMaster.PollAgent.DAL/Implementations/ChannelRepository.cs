using System.Collections.Generic;
using System.Linq;

using Microsoft.EntityFrameworkCore;

using ModbusMaster.Domain.Entities;
using ModbusMaster.PollAgent.DAL.Interfaces;

namespace ModbusMaster.PollAgent.DAL.Implementations
{
    internal class ChannelRepository : Repository<Channel>, IChannelRepository
    {
        public ChannelRepository(ModbusPollContext context) : base(context)
        {
        }

        public List<Channel> GetConfig()
        {
            return _context.Set<Channel>().Include(x => x.Devices)
                    .ThenInclude(y => y.Registers)
                    .ToList();
        }
    }
}
