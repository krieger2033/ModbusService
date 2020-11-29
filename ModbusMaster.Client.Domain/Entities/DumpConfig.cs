using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ModbusMaster.Domain.Entities;

namespace ModbusMaster.Client.Domain.Entities
{
    public class DumpConfig : Dump
    {
        [ForeignKey("DeviceId")]
        public new DeviceConfig Device { get; set; }
    }
}