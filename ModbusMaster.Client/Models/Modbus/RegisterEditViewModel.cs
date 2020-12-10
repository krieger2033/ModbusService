using System.ComponentModel.DataAnnotations;
using ModbusMaster.Domain.Entities;

namespace ModbusMaster.Client.Models.Modbus
{
    public class RegisterEditViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required] 
        public virtual RegisterType Type { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        [Range(0, ushort.MaxValue)]
        public ushort Offset { get; set; }

        [Required]
        [Range(1, ushort.MaxValue)]
        public ushort Count { get; set; }
    }
}