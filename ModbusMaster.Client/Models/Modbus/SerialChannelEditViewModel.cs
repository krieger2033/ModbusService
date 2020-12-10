using System.ComponentModel.DataAnnotations;
using System.IO.Ports;
using ModbusMaster.Domain.Entities;

namespace ModbusMaster.Client.Models.Modbus
{
    public class SerialChannelEditViewModel : ChannelEditViewModel
    {
        [Required]
        public string ComPort { get; set; }

        [Required]
        public int? Baudrate { get; set; }

        public Parity? Parity { get; set; }

        public StopBits? StopBits { get; set; }
    }
}