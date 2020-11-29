using System.ComponentModel.DataAnnotations;
using System.IO.Ports;
using ModbusMaster.Domain.Entities;

namespace ModbusMaster.Client.Models.Modbus
{
    public class SerialChannelCreateViewModel : ChannelCreateViewModel
    {
        [Required] 
        public override ChannelType Type { get; set; } = ChannelType.SerialPort;

        [Required]
        public string ComPort { get; set; }

        [Required]
        public int Baudrate { get; set; } = 9600;

        public Parity? Parity { get; set; }

        public StopBits? StopBits { get; set; }
    }
}