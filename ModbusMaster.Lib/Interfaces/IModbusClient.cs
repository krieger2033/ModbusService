using System;
using System.Collections;
using System.Threading.Tasks;

using NModbus;

using ModbusMaster.Domain.Entities;

namespace ModbusMaster.Lib.Interfaces
{
    public interface IModbusClient : IDisposable
    {
        IModbusMaster ModbusMaster { get; }

        void SetSlave(Device device);

        bool Connect();

        void Disconnect();

        #region Read Functions
        bool[] ReadCoils(ushort startAddress, ushort numberOfPoints);
        Task<bool[]> ReadCoilsAsync(ushort startAddress, ushort numberOfPoints);
        ushort[] ReadHoldingRegisters(ushort startAddress, ushort numberOfPoints);
        Task<ushort[]> ReadHoldingRegistersAsync(ushort startAddress, ushort numberOfPoints);
        ushort[] ReadInputRegisters(ushort startAddress, ushort numberOfPoints);
        Task<ushort[]> ReadInputRegistersAsync(ushort startAddress, ushort numberOfPoints);
        bool[] ReadInputs(ushort startAddress, ushort numberOfPoints);
        Task<bool[]> ReadInputsAsync(ushort startAddress, ushort numberOfPoints);
        #endregion

        #region Write Functions
        void WriteSingleCoil(ushort coilAddress, bool value);
        Task WriteSingleCoilAsync(ushort coilAddress, bool value);
        void WriteMultipleCoils(ushort startAddress, bool[] data);
        Task WriteMultipleCoilsAsync(ushort startAddress, bool[] data);
        void WriteSingleRegister(ushort registerAddress, ushort value);
        Task WriteSingleRegisterAsync(ushort registerAddress, ushort value);
        void WriteMultipleRegisters(ushort startAddress, ushort[] data);
        Task WriteMultipleRegistersAsync(ushort startAddress, ushort[] data);
        #endregion
    }
}