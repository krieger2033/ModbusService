using ModbusMaster.PollAgent.Lib.Implementations;

namespace ModbusMaster.PollAgent.Lib.Interfaces
{
    public interface ITcpClientSettings
    {
        TcpMasterData TcpMaster { get; set; }
        TcpSlaveData TcpSlave { get; set; }
    }
}
