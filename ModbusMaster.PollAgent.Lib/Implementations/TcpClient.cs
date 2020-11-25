using System;
using System.Threading.Tasks;
using ModbusMaster.Domain.Entities;
using ModbusMaster.PollAgent.Lib.Interfaces;
using NModbus;
using NModbus.IO;

namespace ModbusMaster.PollAgent.Lib.Implementations
{
    public class TcpClient : ITcpClient
    {
        #region Private Data Members

        private System.Net.Sockets.TcpClient _client;
        private readonly ModbusFactory _factory;
        private IModbusMaster _modbus;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the NModbus master.
        /// </summary>
        public IModbusMaster ModbusMaster { get => _modbus; }

        /// <summary>
        /// Gets or sets the TCP/IP Modbus master data.
        /// </summary>
        public TcpMasterData TcpMaster { get; set; } = new TcpMasterData();

        /// <summary>
        /// Gets or sets the TCP/IP Modbus slave data.
        /// </summary>
        public TcpSlaveData TcpSlave { get; set; } = new TcpSlaveData();

        /// <summary>
        /// Gets or sets the swap byte flag.
        /// </summary>
        public bool SwapBytes { get; set; }

        /// <summary>
        /// Gets or sets the swap word flag.
        /// </summary>
        public bool SwapWords { get; set; }

        /// <summary>
        /// Gets a value indicating whether the socket is connected to a host.
        /// </summary>
        public bool Connected { get => _client?.Connected ?? false; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TcpClient"/> class.
        /// </summary>
        public TcpClient()
        {
            _factory = new ModbusFactory();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Connects the client and returns the Modbus _modbus.
        /// </summary>
        /// <returns></returns>
        public bool Connect()
        {
            try
            {
                _client = new System.Net.Sockets.TcpClient();

                if (_client != null)
                {
                    _client.ExclusiveAddressUse = TcpMaster.ExclusiveAddressUse;
                    _client.ReceiveTimeout = TcpMaster.ReceiveTimeout;
                    _client.SendTimeout = TcpMaster.SendTimeout;
                    _client.Connect(TcpSlave.Address, TcpSlave.Port);

                    if (_client.Connected)
                    {
                        var adapter = new TcpClientAdapter(_client);
                        var transport = GetModbusTransport(adapter);
                        _modbus = _factory.CreateIpMaster(transport);
                        return true;
                    }
                    else
                    {
                        _client.Dispose();
                        _client = null;
                    }
                }
            }
            catch (Exception)
            {
                _client?.Dispose();
                _client = null;
            }

            _modbus = null;
            return false;
        }

        /// <summary>
        /// Disconnects the client and disposes the Modbus instance.
        /// </summary>
        public void Disconnect()
        {
            if (_client?.Connected ?? false)
            {
                _modbus?.Dispose();
                _modbus = null;
            }
        }

        public void Dispose()
        {
            Disconnect();
        }

        public void SetSlave(Device device)
        {
            TcpSlave = new TcpSlaveData()
            {
                Address = device.Ip,
                Port = device.Port.Value,
                Type = device.Type
            };
        }

        #endregion

        #region Private Methods

        private IModbusTransport GetModbusTransport(IStreamResource adapter)
        {
            switch(TcpSlave.Type)
            {
                case DeviceType.ModbusRTU: return _factory.CreateRtuTransport(adapter);

                case DeviceType.ModbusTCP:
                default: return _factory.CreateIpTransport(adapter);
            }
        }

        #endregion

        #region Modbus Functions

        #region Read Functions

        /// <summary>
        /// Reads from 1 to 2000 contiguous coils status.
        /// </summary>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="numberOfPoints">Number of coils to read.</param>
        /// <returns>Coils status.</returns>
        public bool[] ReadCoils(ushort startAddress, ushort numberOfPoints)
            => _modbus.ReadCoils(TcpSlave.ID, startAddress, numberOfPoints);

        /// <summary>
        /// Asynchronously reads from 1 to 2000 contiguous coils status.
        /// </summary>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="numberOfPoints">Number of coils to read.</param>
        /// <returns>A task that represents the asynchronous read operation.</returns>
        public Task<bool[]> ReadCoilsAsync(ushort startAddress, ushort numberOfPoints)
            => _modbus.ReadCoilsAsync(TcpSlave.ID, startAddress, numberOfPoints);

        /// <summary>
        /// Reads contiguous block of holding registers.
        /// </summary>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="numberOfPoints">Number of holding registers to read.</param>
        /// <returns>Holding registers status.</returns>
        public ushort[] ReadHoldingRegisters(ushort startAddress, ushort numberOfPoints)
            => _modbus.ReadHoldingRegisters(TcpSlave.ID, startAddress, numberOfPoints);

        /// <summary>
        /// Asynchronously reads contiguous block of holding registers.
        /// </summary>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="numberOfPoints">Number of holding registers to read.</param>
        /// <returns>A task that represents the asynchronous read operation.</returns>
        public Task<ushort[]> ReadHoldingRegistersAsync(ushort startAddress, ushort numberOfPoints)
            => _modbus.ReadHoldingRegistersAsync(TcpSlave.ID, startAddress, numberOfPoints);

        /// <summary>
        /// Reads contiguous block of input registers.
        /// </summary>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="numberOfPoints">Number of holding registers to read.</param>
        /// <returns>Input registers status.</returns>
        public ushort[] ReadInputRegisters(ushort startAddress, ushort numberOfPoints)
            => _modbus.ReadInputRegisters(TcpSlave.ID, startAddress, numberOfPoints);

        /// <summary>
        /// Asynchronously reads contiguous block of input registers.
        /// </summary>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="numberOfPoints">Number of holding registers to read.</param>
        /// <returns>A task that represents the asynchronous read operation.</returns>
        public Task<ushort[]> ReadInputRegistersAsync(ushort startAddress, ushort numberOfPoints)
            => _modbus.ReadInputRegistersAsync(TcpSlave.ID, startAddress, numberOfPoints);

        /// <summary>
        /// Reads from 1 to 2000 contiguous discrete input status.
        /// </summary>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="numberOfPoints">Number of discrete inputs to read.</param>
        /// <returns>Discrete inputs status.</returns>
        public bool[] ReadInputs(ushort startAddress, ushort numberOfPoints)
            => _modbus.ReadInputs(TcpSlave.ID, startAddress, numberOfPoints);

        /// <summary>
        /// Asynchronously reads from 1 to 2000 contiguous discrete input status.
        /// </summary>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="numberOfPoints">Number of discrete inputs to read.</param>
        /// <returns>A task that represents the asynchronous read operation.</returns>
        public Task<bool[]> ReadInputsAsync(ushort startAddress, ushort numberOfPoints)
            => _modbus.ReadInputsAsync(TcpSlave.ID, startAddress, numberOfPoints);

        #endregion

        #region Write Functions

        /// <summary>
        /// Performs a combination of one read operation and one write operation in a single
        /// Modbus transaction. The write operation is performed before the read.
        /// </summary>
        /// <param name="startReadAddress">Address to begin reading (Holding registers are addressed starting at 0).</param>
        /// <param name="numberOfPointsToRead">Number of registers to read.</param>
        /// <param name="startWriteAddress">Address to begin writing (Holding registers are addressed starting at 0).</param>
        /// <param name="writeData">Register values to write.</param>
        /// <returns>Holding registers status.</returns>
        public ushort[] ReadWriteMultipleRegisters(ushort startReadAddress, ushort numberOfPointsToRead, ushort startWriteAddress, ushort[] writeData)
            => _modbus.ReadWriteMultipleRegisters(TcpSlave.ID, startReadAddress, numberOfPointsToRead, startWriteAddress, writeData);

        /// <summary>
        /// Asynchronously performs a combination of one read operation and one write operation
        /// in a single Modbus transaction. The write operation is performed before the read.
        /// </summary>
        /// <param name="startReadAddress">Address to begin reading (Holding registers are addressed starting at 0).</param>
        /// <param name="numberOfPointsToRead">Number of registers to read.</param>
        /// <param name="startWriteAddress">Address to begin writing (Holding registers are addressed starting at 0).</param>
        /// <param name="writeData">Register values to write.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public Task<ushort[]> ReadWriteMultipleRegistersAsync(ushort startReadAddress, ushort numberOfPointsToRead, ushort startWriteAddress, ushort[] writeData)
            => _modbus.ReadWriteMultipleRegistersAsync(TcpSlave.ID, startReadAddress, numberOfPointsToRead, startWriteAddress, writeData);

        /// <summary>
        /// Writes a sequence of coils.
        /// </summary>
        /// <param name="startAddress">Address to begin writing values.</param>
        /// <param name="data">Values to write.</param>
        public void WriteMultipleCoils(ushort startAddress, bool[] data)
            => _modbus.WriteMultipleCoils(TcpSlave.ID, startAddress, data);

        /// <summary>
        /// Asynchronously writes a sequence of coils.
        /// </summary>
        /// <param name="startAddress">Address to begin writing values.</param>
        /// <param name="data">Values to write.</param>
        /// <returns>A task that represents the asynchronous write operation.</returns>
        public Task WriteMultipleCoilsAsync(ushort startAddress, bool[] data)
            => _modbus.WriteMultipleCoilsAsync(TcpSlave.ID, startAddress, data);

        /// <summary>
        /// Writes a block of 1 to 123 contiguous registers.
        /// </summary>
        /// <param name="startAddress">Address to begin writing values.</param>
        /// <param name="data">Values to write.</param>
        public void WriteMultipleRegisters(ushort startAddress, ushort[] data)
            => _modbus.WriteMultipleRegisters(TcpSlave.ID, startAddress, data);

        /// <summary>
        /// Asynchronously writes a block of 1 to 123 contiguous registers.
        /// </summary>
        /// <param name="startAddress">Address to begin writing values.</param>
        /// <param name="data">Values to write.</param>
        /// <returns>A task that represents the asynchronous write operation.</returns>
        public Task WriteMultipleRegistersAsync(ushort startAddress, ushort[] data)
            => _modbus.WriteMultipleRegistersAsync(TcpSlave.ID, startAddress, data);

        /// <summary>
        /// Writes a single coil value.
        /// </summary>
        /// <param name="coilAddress">Address to write value to.</param>
        /// <param name="value">Value to write.</param>
        public void WriteSingleCoil(ushort coilAddress, bool value)
            => _modbus.WriteSingleCoil(TcpSlave.ID, coilAddress, value);

        /// <summary>
        /// Asynchronously writes a single coil value.
        /// </summary>
        /// <param name="coilAddress">Address to write value to.</param>
        /// <param name="value">Value to write.</param>
        /// <returns>A task that represents the asynchronous write operation.</returns>
        public Task WriteSingleCoilAsync(ushort coilAddress, bool value)
            => _modbus.WriteSingleCoilAsync(TcpSlave.ID, coilAddress, value);

        /// <summary>
        /// Writes a single holding register.
        /// </summary>
        /// <param name="registerAddress">Address to write.</param>
        /// <param name="value">Value to write.</param>
        public void WriteSingleRegister(ushort registerAddress, ushort value)
            => _modbus.WriteSingleRegister(TcpSlave.ID, registerAddress, value);

        /// <summary>
        /// Asynchronously writes a single holding register.
        /// </summary>
        /// <param name="registerAddress">Address to write.</param>
        /// <param name="value">Value to write.</param>
        /// <returns>A task that represents the asynchronous write operation.</returns>
        public Task WriteSingleRegisterAsync(ushort registerAddress, ushort value)
            => _modbus.WriteSingleRegisterAsync(TcpSlave.ID, registerAddress, value);

        #endregion

        #endregion
    }
}
