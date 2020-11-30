using System.ComponentModel.DataAnnotations;
using System.IO.Ports;
using ModbusMaster.Domain.Entities;

namespace ModbusMaster.Client.Models.Modbus
{
    public class RtuDeviceCreateViewModel : DeviceCreateViewModel
    {
        [Required] 
        public override DeviceType Type { get; set; } = DeviceType.ModbusRTU;

        [Required]
        [Range(1, 258)]
        public byte Identificator { get; set; }
    }
}