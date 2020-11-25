using System;
using System.IO.Ports;
using System.Threading.Tasks;
using ModbusMaster.Domain.Entities;
using ModbusMaster.PollAgent.Lib.Interfaces;
using NModbus;
using NModbus.Serial;

namespace ModbusMaster.PollAgent.Lib.Implementations
{
    public class RtuClient : IRtuClient
    {
        #region Private Data Members

        private SerialPort _serialport;
        private readonly ModbusFactory _factory;
        private IModbusMaster _modbus;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the NModbus master.
        /// </summary>
        public IModbusMaster ModbusMaster { get => _modbus; }

        /// <summary>
        /// Gets or sets the Modbus RTU master data.
        /// </summary>
        public RtuMasterData RtuMaster { get; set; } = new RtuMasterData();

        /// <summary>
        /// Gets or sets the Modbus RTU slave slave data.
        /// </summary>
        public RtuSlaveData RtuSlave { get; set; } = new RtuSlaveData();

        /// <summary>
        /// Gets or sets the swap byte flag.
        /// </summary>
        public bool SwapBytes { get; set; }

        /// <summary>
        /// Gets or sets the swap word flag.
        /// </summary>
        public bool SwapWords { get; set; }

        /// <summary>
        /// Gets a value indicating whether the serial port is open.
        /// </summary>
        public bool Connected { get => _serialport?.IsOpen ?? false; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RtuClient"/> class.
        /// </summary>
        public RtuClient()
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
                _serialport = new SerialPort(RtuMaster.SerialPort,
                                             RtuMaster.Baudrate,
                                             RtuMaster.Parity,
                                             RtuMaster.DataBits,
                                             RtuMaster.StopBits);
                if (_serialport != null)
                {
                    _serialport.Open();

                    if (_serialport.IsOpen)
                    {
                        var adapter = new SerialPortAdapter(_serialport);
                        _modbus = _factory.CreateRtuMaster(adapter);
                        _modbus.Transport.SlaveBusyUsesRetryCount = true;
                        _modbus.Transport.Retries = RtuMaster.Retries;
                        _modbus.Transport.WaitToRetryMilliseconds = RtuMaster.WaitToRetryMilliseconds;
                        _modbus.Transport.ReadTimeout = RtuMaster.ReadTimeout;
                        _modbus.Transport.WriteTimeout = RtuMaster.WriteTimeout;
                        return true;
                    }
                    else
                    {
                        _serialport.Dispose();
                        _serialport = null;
                    }
                }
            }
            catch (Exception)
            {
                _serialport?.Dispose();
                _serialport = null;
            }

            _modbus = null;
            return false;
        }

        /// <summary>
        /// Disconnects the serial port and disposes the Modbus instance.
        /// </summary>
        public void Disconnect()
        {
            if (_serialport?.IsOpen ?? false)
            {
                _serialport.Close();
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
            RtuSlave = new RtuSlaveData()
            {
                ID = device.Identificator.Value
            };
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
            => _modbus.ReadCoils(RtuSlave.ID, startAddress, numberOfPoints);

        /// <summary>
        /// Asynchronously reads from 1 to 2000 contiguous coils status.
        /// </summary>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="numberOfPoints">Number of coils to read.</param>
        /// <returns>A task that represents the asynchronous read operation.</returns>
        public Task<bool[]> ReadCoilsAsync(ushort startAddress, ushort numberOfPoints)
            => _modbus.ReadCoilsAsync(RtuSlave.ID, startAddress, numberOfPoints);

        /// <summary>
        /// Reads contiguous block of holding registers.
        /// </summary>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="numberOfPoints">Number of holding registers to read.</param>
        /// <returns>Holding registers status.</returns>
        public ushort[] ReadHoldingRegisters(ushort startAddress, ushort numberOfPoints)
            => _modbus.ReadHoldingRegisters(RtuSlave.ID, startAddress, numberOfPoints);

        /// <summary>
        /// Asynchronously reads contiguous block of holding registers.
        /// </summary>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="numberOfPoints">Number of holding registers to read.</param>
        /// <returns>A task that represents the asynchronous read operation.</returns>
        public Task<ushort[]> ReadHoldingRegistersAsync(ushort startAddress, ushort numberOfPoints)
            => _modbus.ReadHoldingRegistersAsync(RtuSlave.ID, startAddress, numberOfPoints);

        /// <summary>
        /// Reads contiguous block of input registers.
        /// </summary>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="numberOfPoints">Number of holding registers to read.</param>
        /// <returns>Input registers status.</returns>
        public ushort[] ReadInputRegisters(ushort startAddress, ushort numberOfPoints)
            => _modbus.ReadInputRegisters(RtuSlave.ID, startAddress, numberOfPoints);

        /// <summary>
        /// Asynchronously reads contiguous block of input registers.
        /// </summary>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="numberOfPoints">Number of holding registers to read.</param>
        /// <returns>A task that represents the asynchronous read operation.</returns>
        public Task<ushort[]> ReadInputRegistersAsync(ushort startAddress, ushort numberOfPoints)
            => _modbus.ReadInputRegistersAsync(RtuSlave.ID, startAddress, numberOfPoints);

        /// <summary>
        /// Reads from 1 to 2000 contiguous discrete input status.
        /// </summary>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="numberOfPoints">Number of discrete inputs to read.</param>
        /// <returns>Discrete inputs status.</returns>
        public bool[] ReadInputs(ushort startAddress, ushort numberOfPoints)
            => _modbus.ReadInputs(RtuSlave.ID, startAddress, numberOfPoints);

        /// <summary>
        /// Asynchronously reads from 1 to 2000 contiguous discrete input status.
        /// </summary>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="numberOfPoints">Number of discrete inputs to read.</param>
        /// <returns>A task that represents the asynchronous read operation.</returns>
        public Task<bool[]> ReadInputsAsync(ushort startAddress, ushort numberOfPoints)
            => _modbus.ReadInputsAsync(RtuSlave.ID, startAddress, numberOfPoints);

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
            => _modbus.ReadWriteMultipleRegisters(RtuSlave.ID, startReadAddress, numberOfPointsToRead, startWriteAddress, writeData);

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
            => _modbus.ReadWriteMultipleRegistersAsync(RtuSlave.ID, startReadAddress, numberOfPointsToRead, startWriteAddress, writeData);

        /// <summary>
        /// Writes a sequence of coils.
        /// </summary>
        /// <param name="startAddress">Address to begin writing values.</param>
        /// <param name="data">Values to write.</param>
        public void WriteMultipleCoils(ushort startAddress, bool[] data)
            => _modbus.WriteMultipleCoils(RtuSlave.ID, startAddress, data);

        /// <summary>
        /// Asynchronously writes a sequence of coils.
        /// </summary>
        /// <param name="startAddress">Address to begin writing values.</param>
        /// <param name="data">Values to write.</param>
        /// <returns>A task that represents the asynchronous write operation.</returns>
        public Task WriteMultipleCoilsAsync(ushort startAddress, bool[] data)
            => _modbus.WriteMultipleCoilsAsync(RtuSlave.ID, startAddress, data);

        /// <summary>
        /// Writes a block of 1 to 123 contiguous registers.
        /// </summary>
        /// <param name="startAddress">Address to begin writing values.</param>
        /// <param name="data">Values to write.</param>
        public void WriteMultipleRegisters(ushort startAddress, ushort[] data)
            => _modbus.WriteMultipleRegisters(RtuSlave.ID, startAddress, data);

        /// <summary>
        /// Asynchronously writes a block of 1 to 123 contiguous registers.
        /// </summary>
        /// <param name="startAddress">Address to begin writing values.</param>
        /// <param name="data">Values to write.</param>
        /// <returns>A task that represents the asynchronous write operation.</returns>
        public Task WriteMultipleRegistersAsync(ushort startAddress, ushort[] data)
            => _modbus.WriteMultipleRegistersAsync(RtuSlave.ID, startAddress, data);

        /// <summary>
        /// Writes a single coil value.
        /// </summary>
        /// <param name="coilAddress">Address to write value to.</param>
        /// <param name="value">Value to write.</param>
        public void WriteSingleCoil(ushort coilAddress, bool value)
            => _modbus.WriteSingleCoil(RtuSlave.ID, coilAddress, value);

        /// <summary>
        /// Asynchronously writes a single coil value.
        /// </summary>
        /// <param name="coilAddress">Address to write value to.</param>
        /// <param name="value">Value to write.</param>
        /// <returns>A task that represents the asynchronous write operation.</returns>
        public Task WriteSingleCoilAsync(ushort coilAddress, bool value)
            => _modbus.WriteSingleCoilAsync(RtuSlave.ID, coilAddress, value);

        /// <summary>
        /// Writes a single holding register.
        /// </summary>
        /// <param name="registerAddress">Address to write.</param>
        /// <param name="value">Value to write.</param>
        public void WriteSingleRegister(ushort registerAddress, ushort value)
            => _modbus.WriteSingleRegister(RtuSlave.ID, registerAddress, value);

        /// <summary>
        /// Asynchronously writes a single holding register.
        /// </summary>
        /// <param name="registerAddress">Address to write.</param>
        /// <param name="value">Value to write.</param>
        /// <returns>A task that represents the asynchronous write operation.</returns>
        public Task WriteSingleRegisterAsync(ushort registerAddress, ushort value)
            => _modbus.WriteSingleRegisterAsync(RtuSlave.ID, registerAddress, value);

        #endregion

        #endregion
    }
}
