using ModbusMaster.Lib.Implementations;

namespace ModbusMaster.Lib.Interfaces
{
    public interface ITcpClientSettings
    {
        TcpMasterData TcpMaster { get; set; }
        TcpSlaveData TcpSlave { get; set; }
    }
}
