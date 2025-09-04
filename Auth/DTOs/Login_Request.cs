using System.ComponentModel.DataAnnotations;

namespace Auth.DTOs
{
    public class Login_Request
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
