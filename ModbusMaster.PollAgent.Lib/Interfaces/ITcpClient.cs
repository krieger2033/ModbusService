namespace ModbusMaster.PollAgent.Lib.Interfaces
{
    public interface ITcpClient : IModbusClient, ITcpClientSettings
    {
        bool SwapBytes { get; set; }
        bool SwapWords { get; set; }
        bool Connected { get; }
    }
}
