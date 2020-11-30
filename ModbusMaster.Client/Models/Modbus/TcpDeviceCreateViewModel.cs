using System.ComponentModel.DataAnnotations;
using ModbusMaster.Domain.Entities;

namespace ModbusMaster.Client.Models.Modbus
{
    public class TcpDeviceCreateViewModel : DeviceCreateViewModel
    {
        [Required]
        public override DeviceType Type { get; set; } = DeviceType.ModbusTCP;

        [Required]
        [RegularExpression(@"^(?:[0-9]{1,3}\.){3}[0-9]{1,3}$")]
        public string Ip { get; set; }

        [Required]
        public int Port { get; set; }
    }
}