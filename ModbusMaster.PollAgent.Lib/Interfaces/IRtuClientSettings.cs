using ModbusMaster.PollAgent.Lib.Implementations;

namespace ModbusMaster.PollAgent.Lib.Interfaces
{
    public interface IRtuClientSettings
    {
        RtuMasterData RtuMaster { get; set; }
        RtuSlaveData RtuSlave { get; set; }
    }
}
