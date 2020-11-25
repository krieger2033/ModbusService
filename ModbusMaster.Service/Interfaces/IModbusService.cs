using System.Collections.Generic;
using System.Threading;
using ModbusMaster.Domain.Entities;

namespace ModbusMaster.PollAgent.Services.Interfaces
{
    public interface IModbusService
    {
        void StartWalkaround(IEnumerable<Channel> channels, CancellationToken stoppingToken);
    }
}
