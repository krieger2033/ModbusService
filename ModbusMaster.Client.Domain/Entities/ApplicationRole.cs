using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace ModbusMaster.Client.Domain.Entities
{
    public class ApplicationRole : IdentityRole
    {
        public ICollection<ApplicationUserRole> UserRoles { get; set; }
    }
}