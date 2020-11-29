using System.Threading.Tasks;
using ModbusMaster.Client.Domain.Entities;
using ModbusMaster.Client.Models.Modbus;

namespace ModbusMaster.Client.Factories.Modbus
{
    public interface IModbusFactory
    {
        Task<ChannelsListViewModel> GetChannelsListViewModel();

        SerialChannelCreateViewModel GetSerialChannelCreateViewModel();

        TcpChannelCreateViewModel GetTcpChannelCreateViewModel();

        ChannelConfig GetChannel(TcpChannelCreateViewModel model);

        ChannelConfig GetChannel(SerialChannelCreateViewModel model);
    }
}