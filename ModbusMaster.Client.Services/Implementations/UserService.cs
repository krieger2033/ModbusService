using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
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
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IHttpContextAccessor _contextAccessor;

        public UserService(UserManager<ApplicationUser> userManager, IServiceScopeFactory scopeFactory, IHttpContextAccessor  contextAccessor)
        {
            _userManager = userManager;
            _scopeFactory = scopeFactory;
            _contextAccessor = contextAccessor;
        }

        private string GetCurrentUserId()
        {
            return _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        }

        public async Task<ApplicationUser> GetUser(string login)
        {
            using var context = _scopeFactory.CreateScope();

            return await _userManager.Users.Where(u => u.UserName == login && u.Id != GetCurrentUserId()).FirstOrDefaultAsync();
        }

        public async Task<List<ApplicationUser>> ReadAll()
        {
            using var context = _scopeFactory.CreateScope();

            return await _userManager.Users
                            .Where(u => u.Id != GetCurrentUserId())
                            .Include(u => u.UserRoles)
                            .ThenInclude(ur => ur.Role)
                            .ToListAsync();
        }

        public async Task<bool> Add(ApplicationUser user)
        {
            IdentityResult result = await _userManager.CreateAsync(user);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "User");
            }

            return result.Succeeded;
        }

        public async Task Remove(ApplicationUser user)
        {
            await _userManager.DeleteAsync(user);
        }
    }
}   