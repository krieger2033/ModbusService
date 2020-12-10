using System.ComponentModel.DataAnnotations;
using System.IO.Ports;
using ModbusMaster.Domain.Entities;

namespace ModbusMaster.Client.Models.Modbus
{
    public class RtuDeviceEditViewModel : DeviceEditViewModel
    {
        [Required]
        [Range(1, 248)]
        public byte? Identificator { get; set; }
    }
}