using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ModbusMaster.Domain.Entities;

namespace ModbusMaster.Client.Domain.Entities
{
    public class RegisterConfig : Register
    {
        [Required]
        public string Title { get; set; }

        [ForeignKey("DeviceId")]
        public new DeviceConfig Device { get; set; }
    }
}