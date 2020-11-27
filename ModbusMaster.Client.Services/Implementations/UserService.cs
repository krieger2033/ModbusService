using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ModbusMaster.Client.DAL.Interfaces;
using ModbusMaster.Client.Domain.Entities;
using ModbusMaster.Client.Services.Interfaces;

namespace ModbusMaster.Client.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IServiceScopeFactory _scopeFactory;

        public UserService(RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager, IServiceScopeFactory scopeFactory)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _scopeFactory = scopeFactory;
        }

        public async Task<List<ApplicationUser>> ReadAll()
        {
            using var context = _scopeFactory.CreateScope();

            return await _userManager.Users
                            .Include(u => u.UserRoles)
                            .ThenInclude(ur => ur.Role)
                            .ToListAsync();
        }
    }
}   