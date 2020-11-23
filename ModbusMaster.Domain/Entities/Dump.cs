using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModbusMaster.Domain.Entities
{
    public class Dump : BaseEntity
    {
        [Required]
        public int DeviceId { get; set; }

        public RegisterType RegisterType { get; set; }

        public ushort Offset { get; set; }

        public ushort? Data { get; set; }

        public DateTimeOffset Date { get; set; }

        [ForeignKey("DeviceId")]
        public virtual Device Device { get; set; }
    }
}