using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using ModbusMaster.Client.DAL.Interfaces;
using ModbusMaster.Client.Domain.Entities;
using ModbusMaster.Client.Services.Interfaces;
using ModbusMaster.Domain.Entities;

namespace ModbusMaster.Client.Services.Implementations
{
    public class ModbusService : IModbusService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ModbusService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<string> GetChannelTitle(int channelId)
        {
            return (await _unitOfWork.ChannelsRepository.GetSingle(c => c.Id == channelId)).Title;
        }

        public async Task<ChannelType> GetChannelType(int channelId)
        {
            return (await _unitOfWork.ChannelsRepository.GetSingle(c => c.Id == channelId)).Type;
        }

        public async Task<string> GetDeviceTitle(int deviceId)
        {
            return (await _unitOfWork.DevicesRepository.GetSingle(d => d.Id == deviceId)).Title;
        }

        public async Task<ChannelConfig> GetChannelById(int id)
        {
            return await _unitOfWork.ChannelsRepository.GetSingle(c => c.Id == id);
        }

        public async Task<DeviceConfig> GetDeviceById(int id)
        {
            return await _unitOfWork.DevicesRepository.GetSingle(d => d.Id == id);
        }

        public async Task<RegisterConfig> GetRegisterById(int id)
        {
            return await _unitOfWork.RegistersRepository.GetSingle(r => r.Id == id);
        }

        public async Task<List<ChannelConfig>> GetModbusConfig()
        {
            return await _unitOfWork.ChannelsRepository.GetConfig();
        }

        public async Task Create(ChannelConfig channel)
        {
            _unitOfWork.ChannelsRepository.Insert(channel);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task Create(DeviceConfig device)
        {
            _unitOfWork.DevicesRepository.Insert(device);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task Create(RegisterConfig register)
        {
            _unitOfWork.RegistersRepository.Insert(register);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task Remove(ChannelConfig channel)
        {
            _unitOfWork.ChannelsRepository.Delete(channel);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task Remove(DeviceConfig device)
        {
            _unitOfWork.DevicesRepository.Delete(device);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task Remove(RegisterConfig register)
        {
            _unitOfWork.RegistersRepository.Delete(register);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}   