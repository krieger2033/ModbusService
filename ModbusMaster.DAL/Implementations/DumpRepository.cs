using System;
using System.Collections.Generic;
using System.Text;

using ModbusMaster.DAL.Interfaces;
using ModbusMaster.Domain.Entities;

namespace ModbusMaster.DAL.Implementations
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

                Dump dump = new Dump() {
                    DeviceId = register.DeviceId,
                    RegisterType = register.Type,
                    Offset = (ushort)(register.Offset + offset),
                    Data = response,
                    Date = DateTime.Now
                };

                Insert(dump);
            }
        }
    }
}
