using ModbusMaster.Domain.Entities;

namespace ModbusMaster.Lib.Implementations
{
    public class TcpSlaveData
    {
        public string Address { get; set; } = "127.0.0.1";
        public int Port { get; set; } = 502;
        public byte ID { get; set; } = 1;
        public DeviceType Type { get; set; } = DeviceType.ModbusTCP;
    }
}
