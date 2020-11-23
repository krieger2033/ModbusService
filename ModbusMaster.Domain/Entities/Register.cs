using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModbusMaster.Domain.Entities
{   
    public class Register : BaseEntity
    {
        [Required]
        public int DeviceId { get; set; }

        [Required]
        public RegisterType Type { get; set; }

        public ushort Offset { get; set; }

        public ushort Count { get; set; }

        public string Formula { get; set; }

        [ForeignKey("DeviceId")]
        public virtual Device Device { get; set; }
    }

    public enum RegisterType
    {
        DiscreteInput = 10001,
        Coil = 20001,
        Input = 30001,
        HoldingRegister = 40001
    }
}