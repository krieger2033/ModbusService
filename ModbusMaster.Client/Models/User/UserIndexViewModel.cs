using Microsoft.AspNetCore.Identity;
using ModbusMaster.Client.Domain.Entities;

namespace ModbusMaster.Client.Models.User
{
    public class UserIndexViewModel
    {
        public string Username { get; set; }

        public string Role { get; set; }
    }
}