namespace ModbusMaster.PollAgent.Lib.Interfaces
{
    public interface IRtuClient : IModbusClient, IRtuClientSettings
    {
        bool SwapBytes { get; set; }
        bool SwapWords { get; set; }
        bool Connected { get; }
    }
}
