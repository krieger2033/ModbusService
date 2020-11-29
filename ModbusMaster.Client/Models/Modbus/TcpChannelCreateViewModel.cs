using System.ComponentModel.DataAnnotations;
using ModbusMaster.Domain.Entities;

namespace ModbusMaster.Client.Models.Modbus
{
    public class TcpChannelCreateViewModel : ChannelCreateViewModel
    {
        [Required] 
        public override ChannelType Type { get; set; } = ChannelType.Tcp;
    }
}