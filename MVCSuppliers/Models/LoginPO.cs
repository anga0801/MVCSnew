using System.ComponentModel.DataAnnotations;

namespace MVCSuppliers.Models
{
    public class LoginPO
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}