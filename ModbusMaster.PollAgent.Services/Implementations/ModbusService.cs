using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using ModbusMaster.Domain.Entities;
using ModbusMaster.PollAgent.DAL.Interfaces;
using ModbusMaster.PollAgent.Lib.Implementations;
using ModbusMaster.PollAgent.Lib.Interfaces;
using ModbusMaster.PollAgent.Services.Interfaces;

namespace ModbusMaster.PollAgent.Services.Implementations
{
    public class ModbusService : IModbusService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ModbusService> _logger;

        public ModbusService(IUnitOfWork unitOfWork, ILogger<ModbusService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public void StartWalkaround(IEnumerable<Channel> channels, CancellationToken stoppingToken)
        {
            Parallel.ForEach(channels, currentChannel => {

                Task task = Task.Run(() => ProcessChannel(currentChannel), stoppingToken);

                bool isTimedOut = task.Wait(1000, stoppingToken);

                if(isTimedOut)
                {
                    _unitOfWork.DumpsRepository.AddEmptyChannelResult(currentChannel, true);

                    _logger.LogError("{date} - Channel {num} timed out", DateTimeOffset.Now, currentChannel.Id);
                }
            });

            _unitOfWork.SaveChanges();
        }

        private void ProcessChannel(Channel channel)
        {
            IModbusClient client = GetModbusClient(channel); // exception(?)

            channel.Devices.ToList().ForEach(device =>
            {
                try
                {
                    ProcessDevice(client, device);
                }
                catch (Exception e)
                {
                    _unitOfWork.DumpsRepository.AddEmptyDeviceResult(device);

                    _logger.LogError("{date} - Device {num} error: {exception}", DateTimeOffset.Now, device.Id, e.Message);
                }
            });
        }

        private void ProcessDevice(IModbusClient client, Device device)
        {
            client.SetSlave(device);
            client.Connect(); // exception: device connection

            device.Registers.ToList().ForEach(register =>
            {
                try
                {
                    ProcessRegisterGroup(client, register); // exception: connection gone away(?), exception: wrong registers data
                }
                catch (Exception e)
                {
                    _unitOfWork.DumpsRepository.AddEmptyRegisterResult(register);

                    _logger.LogError("{date} - Register group {num} error: {exception}", DateTimeOffset.Now, register.Id, e.Message);
                }

                //_logger.LogInformation("{date} - Device {num} processed", DateTimeOffset.Now, device.Id);
            });

            client.Disconnect();
        }

        private void ProcessRegisterGroup(IModbusClient client, Register register)
        {
            var dumpRepository = _unitOfWork.DumpsRepository;

            switch (register.Type) 
            {
                case RegisterType.Coil:
                    dumpRepository.AddRegisterResult(register, client.ReadCoils(register.Offset, register.Count));
                    break;
                case RegisterType.DiscreteInput:
                    dumpRepository.AddRegisterResult(register, client.ReadInputRegisters(register.Offset, register.Count));
                    break;
                case RegisterType.HoldingRegister:
                    dumpRepository.AddRegisterResult(register, client.ReadHoldingRegisters(register.Offset, register.Count));
                    break;
                case RegisterType.Input:
                    dumpRepository.AddRegisterResult(register, client.ReadInputs(register.Offset, register.Count));
                    break;
            }

            //_logger.LogInformation("{date} - Register {num} processed", DateTimeOffset.Now, register.Id);
        }

        private IModbusClient GetModbusClient(Channel channel)
        {
            switch(channel.Type)
            {
                case ChannelType.ModbusTCP: return new TcpClient();
                case ChannelType.SerialPort: 
                    return new RtuClient() 
                    {
                        RtuMaster = new RtuMasterData()
                        {
                            SerialPort = channel.ComPort,
                            Baudrate = channel.Baudrate.Value,
                            Parity = channel.Parity.Value,
                            StopBits = channel.StopBits.Value,
                        }
                    };
            }

            return null;
        }
    }
}


