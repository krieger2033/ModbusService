using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ModbusMaster.Domain.Entities;

namespace ModbusMaster.Client.Domain.Entities
{
    public class ChannelConfig : Channel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public new ICollection<DeviceConfig> Devices { get; set; }
    }
}