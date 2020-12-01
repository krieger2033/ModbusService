using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;

using ModbusMaster.Client.Domain.Entities;
using ModbusMaster.Client.Models.Modbus;
using ModbusMaster.Client.Services.Interfaces;
using ModbusMaster.Domain.Entities;

namespace ModbusMaster.Client.Factories.Modbus
{
    public class ModbusFactory : IModbusFactory
    {
        private readonly IUserService _userService;
        private readonly IModbusService _modbusService;

        public ModbusFactory(IUserService userService, IModbusService modbusService)
        {
            _userService = userService;
            _modbusService = modbusService;
        }

        public async Task<ChannelsListViewModel> GetChannelsListViewModel()
        {
            return ChannelsToChannelsListViewModels(await _modbusService.GetModbusConfig());
        }

        private ChannelsListViewModel ChannelsToChannelsListViewModels(List<ChannelConfig> channels)
        {
            List<ChannelIndexViewModel> channelIndexViewModels = new List<ChannelIndexViewModel>();

            channels.ForEach(channel =>
            {
                var viewModel = new ChannelIndexViewModel()
                {
                    Id = channel.Id,
                    Title = channel.Title,
                    DevicesListViewModel = DevicesToDevicesListViewModel(channel.Devices.ToList())
                };

                channelIndexViewModels.Add(viewModel);
            });

            return new ChannelsListViewModel()
            {
                ChannelIndexViewModels = channelIndexViewModels
            };
        }

        private DevicesListViewModel DevicesToDevicesListViewModel(List<DeviceConfig> devices)
        {
            List<DeviceIndexViewModel> deviceIndexViewModels = new List<DeviceIndexViewModel>();

            devices.ForEach(device =>
            {
                var viewModel = new DeviceIndexViewModel()
                {
                    Id = device.Id,
                    Title = device.Title,
                    RegistersListViewModel = RegistersToRegistersListViewModel(device.Registers.ToList())
                };

                deviceIndexViewModels.Add(viewModel);
            });

            return new DevicesListViewModel()
            {
                DeviceIndexViewModels = deviceIndexViewModels
            };
        }

        private RegistersListViewModel RegistersToRegistersListViewModel(List<RegisterConfig> registers)
        {
            List<RegisterIndexViewModel> registerIndexViewModels = new List<RegisterIndexViewModel>();

            registers.ForEach(register =>
            {
                var viewModel = new RegisterIndexViewModel()
                {
                    Id = register.Id,
                    Title = register.Title
                };

                registerIndexViewModels.Add(viewModel);
            });

            return new RegistersListViewModel()
            {
                RegisterIndexViewModels = registerIndexViewModels
            };
        }

        public SerialChannelCreateViewModel GetSerialChannelCreateViewModel()
        {
            return new SerialChannelCreateViewModel();
        }

        public TcpChannelCreateViewModel GetTcpChannelCreateViewModel()
        {
            return new TcpChannelCreateViewModel();
        }

        public ChannelConfig GetChannel(TcpChannelCreateViewModel model)
        {
            return new ChannelConfig()
            {
                Title = model.Title,
                Type = model.Type
            };
        }

        public ChannelConfig GetChannel(SerialChannelCreateViewModel model)
        {
            return new ChannelConfig()
            {
                Title = model.Title,
                Type = model.Type,
                Baudrate = model.Baudrate,
                ComPort = model.ComPort,
                Parity = model.Parity,
                StopBits = model.StopBits
            };
        }

        public async Task<TcpDeviceCreateViewModel> GetTcpDeviceCreateViewModel(int channelId)
        {
            return new TcpDeviceCreateViewModel()
            {
                ChannelId = channelId,
                ChannelTitle = await _modbusService.GetChannelTitle(channelId)
            };
        }

        public async Task<RtuDeviceCreateViewModel> GetRtuDeviceCreateViewModel(int channelId)
        {
            return new RtuDeviceCreateViewModel()
            {
                ChannelId = channelId,
                ChannelTitle = await _modbusService.GetChannelTitle(channelId)
            };
        }

        public DeviceConfig GetDevice(TcpDeviceCreateViewModel model)
        {
            return new DeviceConfig()
            {
                ChannelId = model.ChannelId,
                Title = model.Title,
                Type = model.Type,
                Ip = model.Ip,
                Port = model.Port
            };
        }

        public DeviceConfig GetDevice(RtuDeviceCreateViewModel model)
        {
            return new DeviceConfig()
            {
                ChannelId = model.ChannelId,
                Title = model.Title,
                Type = DeviceType.ModbusRTU,
                Identificator = model.Identificator
            };
        }

        public async Task<RegisterCreateViewModel> GetRegisterCreateViewModel(int deviceId)
        {
            return new RegisterCreateViewModel()
            {
                DeviceId = deviceId,
                DeviceTitle = await _modbusService.GetDeviceTitle(deviceId)
            };
        }

        public RegisterConfig GetRegister(RegisterCreateViewModel model)
        {
            return new RegisterConfig()
            {
                DeviceId = model.DeviceId,
                Title = model.Title,
                Type = model.Type,
                Offset = model.Offset,
                Count = model.Count
            };
        }
    }
}