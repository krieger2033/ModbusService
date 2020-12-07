using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace ModbusMaster.Client.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<ApplicationUserRole> UserRoles { get; set; }
    }
}