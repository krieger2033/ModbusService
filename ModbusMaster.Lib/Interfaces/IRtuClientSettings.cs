using ModbusMaster.Lib.Implementations;

namespace ModbusMaster.Lib.Interfaces
{
    public interface IRtuClientSettings
    {
        RtuMasterData RtuMaster { get; set; }
        RtuSlaveData RtuSlave { get; set; }
    }
}
