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

        SerialChannelEditViewModel GetSerialChannelEditViewModel(ChannelConfig channel);

        TcpChannelEditViewModel GetTcpChannelEditViewModel(ChannelConfig channel);

        Task<ChannelConfig> UpdateChannel(TcpChannelEditViewModel model);

        Task<ChannelConfig> UpdateChannel(SerialChannelEditViewModel model);


        Task<TcpDeviceCreateViewModel> GetTcpDeviceCreateViewModel(int channelId);

        Task<RtuDeviceCreateViewModel> GetRtuDeviceCreateViewModel(int channelId);

        DeviceConfig GetDevice(TcpDeviceCreateViewModel model);

        DeviceConfig GetDevice(RtuDeviceCreateViewModel model);

        RtuDeviceEditViewModel GetRtuDeviceEditViewModel(DeviceConfig device);

        TcpDeviceEditViewModel GetTcpDeviceEditViewModel(DeviceConfig device);

        Task<DeviceConfig> UpdateDevice(TcpDeviceEditViewModel model);

        Task<DeviceConfig> UpdateDevice(RtuDeviceEditViewModel model);


        Task<RegisterCreateViewModel> GetRegisterCreateViewModel(int deviceId);

        RegisterConfig GetRegister(RegisterCreateViewModel model);

        RegisterEditViewModel GetRegisterEditViewModel(RegisterConfig register);

        Task<RegisterConfig> UpdateRegister(RegisterEditViewModel model);
    }
}