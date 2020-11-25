using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.EntityFrameworkCore;

using ModbusMaster.DAL;
using ModbusMaster.Domain.Entities;
using ModbusMaster.PollAgent.DAL.Interfaces;

namespace ModbusMaster.PollAgent.DAL.Implementations
{
    internal class DumpRepository : Repository<Dump>, IDumpRepository
    {
        public DumpRepository(ModbusDumpContext context) : base(context)
        {
        }

        public void AddRegisterResult(Register register, bool[] result)
        {
            ushort[] convertedResult = Array.ConvertAll(result, Convert.ToUInt16);

            AddRegisterResult(register, convertedResult);
        }

        public void AddRegisterResult(Register register, ushort[] result)
        {
            for(ushort offset = 0; offset < register.Count; offset++)
            {
                ushort? response = null;
                if (offset < result.Length) { response = result[offset]; }

                AddDumpRecord(register, offset, response);
            }
        }

        //TODO: Optimisation
        public void AddEmptyChannelResult(Channel channel, bool isTimedOut)
        {
            if(!isTimedOut) { AddEmptyChannelResult(channel); }

            var unsavedDumps = GetUnsavedDumps();

            channel.Devices.ToList().ForEach(device => { device.Registers.ToList().ForEach(register => {
                for (ushort offset = 0; offset < register.Count; offset++)
                {
                    bool isAlreadyAdded = unsavedDumps.Exists(x => x.DeviceId == register.DeviceId && x.RegisterType == register.Type && x.Offset == register.Offset + offset);

                    if (!isAlreadyAdded) { AddDumpRecord(register, offset); }
                }
            });});
        }

        public void AddEmptyChannelResult(Channel channel)
        {
            foreach(Device device in channel.Devices)
            {
                AddEmptyDeviceResult(device);
            }
        }

        public void AddEmptyDeviceResult(Device device)
        {
            foreach (Register register in device.Registers)
            {
                AddEmptyRegisterResult(register);
            }
        }

        public void AddEmptyRegisterResult(Register register)
        {
            for (ushort offset = 0; offset < register.Count; offset++)
            {
                AddDumpRecord(register, offset);
            }
        }

        private void AddDumpRecord(Register register, ushort offset, ushort? data = null)
        {
            Dump dump = new Dump()
            {
                DeviceId = register.DeviceId,
                RegisterType = register.Type,
                Offset = (ushort)(register.Offset + offset),
                Data = data,
                Date = DateTime.Now
            };

            Insert(dump);
        }

        private List<Dump> GetUnsavedDumps()
        {
            return _context.ChangeTracker.Entries<Dump>()
                    .Where(x => x.State == EntityState.Added)
                    .Select(y => y.Entity)
                    .ToList();
        }
    }
}
