using ModbusMaster.Domain.Entities;
using System.Collections.Generic;

using System.Threading;

namespace ModbusMaster.Service.Interfaces
{
    public interface IModbusService
    {
        void StartWalkaround(IEnumerable<Channel> channels, CancellationToken stoppingToken);
    }
}
