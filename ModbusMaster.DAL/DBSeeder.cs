﻿using ModbusMaster.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ModbusMaster.DAL
{
    public static class DBSeeder
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Channel>().HasData
            (
                new Channel()
                {
                    Id = 1,
                    Type = ChannelType.ModbusTCP,
                }
            );

            modelBuilder.Entity<Device>().HasData
            (
                new Device()
                {
                    Id = 1,
                    ChannelId = 1,
                    Type = DeviceType.ModbusTCP,
                    Ip = "127.0.0.1",
                    Port = 502,
                },
                new Device()
                {
                    Id = 2,
                    ChannelId = 1,
                    Type = DeviceType.ModbusTCP,
                    Ip = "127.0.0.2",
                    Port = 502,
                }
            );

            modelBuilder.Entity<Register>().HasData
            (
                new Register()
                {
                    Id = 1,
                    DeviceId = 1,
                    Type = RegisterType.Coil,
                    Offset = 0,
                    Count = 5,
                },
                new Register()
                {
                    Id = 2,
                    DeviceId = 2,
                    Type = RegisterType.DiscreteInput,
                    Offset = 0,
                    Count = 3,
                }
            );
        }
    }
}
