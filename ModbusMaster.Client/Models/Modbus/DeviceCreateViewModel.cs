using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO.Ports;
using Microsoft.AspNetCore.Mvc.Rendering;
using ModbusMaster.Domain.Entities;

namespace ModbusMaster.Client.Models.Modbus
{
    public abstract class DeviceCreateViewModel
    {
        public string ChannelTitle { get; set; }

        [Required]
        public int ChannelId { get; set; }

        [Required]
        public string Title { get; set; }
    }
}