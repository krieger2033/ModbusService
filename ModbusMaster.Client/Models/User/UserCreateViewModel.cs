using System.ComponentModel.DataAnnotations;

namespace ModbusMaster.Client.Models.User
{
    public class UserCreateViewModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set;  }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
        [Required]
        [Compare("Password")]
        public string PasswordConfirm { get; set; }
    }
}