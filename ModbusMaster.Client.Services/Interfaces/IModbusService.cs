using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using ModbusMaster.Client.Domain.Entities;
using ModbusMaster.Domain.Entities;

//using ModbusMaster.Client.Domain.Entities;

namespace ModbusMaster.Client.Services.Interfaces
{
    public interface IModbusService
    {
        Task<string> GetChannelTitle(int channelId);

        Task<ChannelType> GetChannelType(int channelId);

        Task<string> GetDeviceTitle(int deviceId);

        Task<ChannelConfig> GetChannelById(int id);

        Task<DeviceConfig> GetDeviceById(int id);

        Task<RegisterConfig> GetRegisterById(int id);

        Task<List<ChannelConfig>> GetModbusConfig();

        Task Create(ChannelConfig channel);

        Task Create(DeviceConfig device);

        Task Create(RegisterConfig register);

        Task Remove(ChannelConfig channel);

        Task Remove(DeviceConfig device);

        Task Remove(RegisterConfig register);

        Task Update(ChannelConfig channel);

        Task Update(DeviceConfig device);

        Task Update(RegisterConfig register);
    }
}