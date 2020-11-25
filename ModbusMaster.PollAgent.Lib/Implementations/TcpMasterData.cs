namespace ModbusMaster.PollAgent.Lib.Implementations
{
    public class TcpMasterData
    {
        public bool ExclusiveAddressUse { get; set; } = true;
        public int ReceiveTimeout { get; set; } = 1000;
        public int SendTimeout { get; set; } = 1000;
    }
}
