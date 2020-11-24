using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
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

        public int GetUnsavedChanges()
        {
            var changes = _context.ChangeTracker.Entries<Dump>()
                            .Where(x => x.State == EntityState.Added)
                            .Select(y => y.Entity)
                            .ToList();

            return _context.Dumps.Local.Count;
        }
    }
}
