using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModbusMaster.Domain.Entities
{
    public class Device : BaseEntity
    {
        [Required]
        public DeviceType Type { get; set; }

        [Required]
        public int ChannelId { get; set; }

        public string Ip { get; set; }

        public int? Port { get; set; }

        public byte? Identificator { get; set; }

        [ForeignKey("ChannelId")]
        public virtual Channel Channel { get; set; }

        public virtual ICollection<Register> Registers { get; set; }

        public virtual ICollection<Dump> Dumps { get; set; }
    }

    public enum DeviceType
    {
        ModbusTCP,
        ModbusRTU
    }
}