using System.Collections.Generic;
using System.IO.Ports;
using System.ComponentModel.DataAnnotations;

namespace ModbusMaster.Domain.Entities
{
    public class Channel : BaseEntity
    {
        [Required]
        public ChannelType Type { get; set; }

        public string ComPort { get; set; }

        public int? Baudrate { get; set; }

        public Parity? Parity { get; set; }

        public StopBits? StopBits { get; set; }

        public virtual ICollection<Device> Devices { get; set; }
    }

    public enum ChannelType
    {
        ModbusTCP,
        SerialPort
    }
}