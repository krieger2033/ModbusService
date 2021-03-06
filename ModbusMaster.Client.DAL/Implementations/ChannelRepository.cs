﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using ModbusMaster.Client.DAL;
using ModbusMaster.Client.DAL.Interfaces;
using ModbusMaster.Client.Domain.Entities;

namespace ModbusMaster.Client.DAL.Implementations
{
    internal class ChannelRepository : Repository<ChannelConfig>, IChannelRepository
    {
        public ChannelRepository(ModbusClientContext context) : base(context)
        {
        }

        public async Task<List<ChannelConfig>> GetConfig()
        {
            return await _context.Set<ChannelConfig>()
                            .Include(x => x.Devices)
                            .ThenInclude(y => y.Registers)
                            .ToListAsync();
        }
    }
}
