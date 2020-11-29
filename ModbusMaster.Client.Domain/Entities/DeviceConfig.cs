using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ModbusMaster.Domain.Entities;

namespace ModbusMaster.Client.Domain.Entities
{
    public class DeviceConfig : Device
    {
        [Required]
        public string Title { get; set; }

        [ForeignKey("ChannelId")]
        public new ChannelConfig Channel { get; set; }

        public new ICollection<RegisterConfig> Registers { get; set; }

        public new ICollection<DumpConfig> Dumps { get; set; }
    }
}