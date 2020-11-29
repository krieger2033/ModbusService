using System.Collections.Generic;

namespace ModbusMaster.Client.Models.Modbus
{
    public class ChannelIndexViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public DevicesListViewModel DevicesListViewModel { get; set; }
    }
}