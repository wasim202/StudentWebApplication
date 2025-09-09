using System.ComponentModel.DataAnnotations;

namespace StudentWebApplication.Models
{
    public class UserLogin
    {
        [Key]
        public int Id { get; set; } 
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
        public Boolean Isadmin { get; set; } 
    }
}
