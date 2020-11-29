using System.Collections.Generic;

namespace ModbusMaster.Client.Models.Modbus
{
    public class DeviceIndexViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public RegistersListViewModel RegistersListViewModel { get; set; }
    }
}