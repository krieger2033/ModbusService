using System;
using System.Collections.Generic;
using System.Text;

using ModbusMaster.Domain.Entities;

namespace ModbusMaster.Api.Domain.Entities
{
    public class AppUser : BaseEntity
    {
        public string UserName { get; set; }
    }
}
