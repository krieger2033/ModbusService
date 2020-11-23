﻿namespace ModbusMaster.Lib.Implementations
{
    public class RtuMasterData
    {
        public string SerialPort { get; set; } = string.Empty;
        public int Baudrate { get; set; } = 9600;
        public System.IO.Ports.Parity Parity { get; set; } = System.IO.Ports.Parity.None;
        public int DataBits { get; set; } = 8;
        public System.IO.Ports.StopBits StopBits { get; set; } = System.IO.Ports.StopBits.One;

        public int ReadTimeout { get; set; } = -1;
        public int WriteTimeout { get; set; } = -1;
        public int Retries { get; set; }
        public int WaitToRetryMilliseconds { get; set; }
        public bool SlaveBusyUsesRetryCount { get; set; }
    }
}
