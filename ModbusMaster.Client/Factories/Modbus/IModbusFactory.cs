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

        Task<TcpDeviceCreateViewModel> GetTcpDeviceCreateViewModel(int channelId);

        Task<RtuDeviceCreateViewModel> GetRtuDeviceCreateViewModel(int channelId);

        DeviceConfig GetDevice(TcpDeviceCreateViewModel model);

        DeviceConfig GetDevice(RtuDeviceCreateViewModel model);

        Task<RegisterCreateViewModel> GetRegisterCreateViewModel(int deviceId);

        RegisterConfig GetRegister(RegisterCreateViewModel model);
    }
}