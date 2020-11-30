using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO.Ports;
using Microsoft.AspNetCore.Mvc.Rendering;
using ModbusMaster.Domain.Entities;

namespace ModbusMaster.Client.Models.Modbus
{
    public class RegisterCreateViewModel
    {
        public string DeviceTitle { get; set; }

        [Required]
        public int DeviceId { get; set; }

        [Required] 
        public virtual RegisterType Type { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        [Range(0, ushort.MaxValue)]
        public ushort Offset { get; set; }

        [Required]
        [Range(0, ushort.MaxValue)]
        public ushort Count { get; set; }
    }
}