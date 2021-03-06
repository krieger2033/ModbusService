﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using ModbusMaster.Client.Domain.Entities;

//using ModbusMaster.Client.Domain.Entities;

namespace ModbusMaster.Client.Services.Interfaces
{
    public interface IUserService
    {
        Task<ApplicationUser> GetUser(string login);

        Task<List<ApplicationUser>> ReadAll();

        Task<bool> Add(ApplicationUser user);

        Task Remove(ApplicationUser user);
    }
}