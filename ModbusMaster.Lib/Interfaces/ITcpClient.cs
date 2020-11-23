using System;
using System.Collections;
using System.Threading.Tasks;

namespace ModbusMaster.Lib.Interfaces
{
    public interface ITcpClient : IModbusClient, ITcpClientSettings
    {
        bool SwapBytes { get; set; }
        bool SwapWords { get; set; }
        bool Connected { get; }
    }
}
