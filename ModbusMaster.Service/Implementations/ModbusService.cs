using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using System.IO.Ports;

using ModbusMaster.DAL.Interfaces;
using ModbusMaster.Domain.Entities;
using ModbusMaster.Service.Interfaces;
using ModbusMaster.Lib.Interfaces;
using ModbusMaster.Lib.Implementations;

using Microsoft.Extensions.Logging;

namespace ModbusMaster.Service.Implemetations
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

                bool res = task.Wait(1000, stoppingToken); // timeout

                _logger.LogInformation("{date} - Channel {num} " + (res ? "processed" : "timed out"), DateTimeOffset.Now, currentChannel.Id);
            });

            _unitOfWork.SaveChanges();
        }

        private void ProcessChannel(Channel channel)
        {
            IModbusClient client = GetModbusClient(channel); // exception(?): wrong channel data

            foreach (Device device in channel.Devices)
            {
                ProcessDevice(client, device);
                //_logger.LogInformation("{date} - Device {num} processed", DateTimeOffset.Now, device.Id);
            }
        }

        private void ProcessDevice(IModbusClient client, Device device)
        {
            client.SetSlave(device);
            client.Connect(); // exception: device connection

            var dumpRepository = _unitOfWork.DumpsRepository;

            foreach (Register register in device.Registers)
            {

                switch (register.Type) // exception: connection gone away(?), exception: wrong registers data
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

            client.Disconnect();
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


