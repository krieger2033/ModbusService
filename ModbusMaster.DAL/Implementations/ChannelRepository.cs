using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.EntityFrameworkCore;

using ModbusMaster.DAL.Interfaces;
using ModbusMaster.Domain.Entities;

namespace ModbusMaster.DAL.Implementations
{
    internal class ChannelRepository : Repository<Channel>
    {
        public ChannelRepository(ModbusDumpContext context) : base(context)
        {
        }

        public override List<Channel> GetAll()
        {
            return _context.Set<Channel>().Include(x => x.Devices).ToList();
        }
    }
}
