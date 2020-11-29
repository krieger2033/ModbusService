using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO.Ports;
using Microsoft.AspNetCore.Mvc.Rendering;
using ModbusMaster.Domain.Entities;

namespace ModbusMaster.Client.Models.Modbus
{
    public abstract class ChannelCreateViewModel
    {
        [Required] 
        public virtual ChannelType Type { get; set; }

        [Required]
        public string Title { get; set; }
    }
}