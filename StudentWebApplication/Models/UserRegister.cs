using System.ComponentModel.DataAnnotations;

namespace StudentWebApplication.Models
{
    public class UserRegister
    {
        [Required]
        public String Username { get; set; }
        [Required, DataType(DataType.Password)]
        public String Password { get; set; }
        [Required, DataType(DataType.Password), Compare("Password")]
        public String ConfirmPassword { get; set; }
        public bool IsAdmin { get; set; } = false;
    }
}
