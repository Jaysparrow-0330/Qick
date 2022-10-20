using System.ComponentModel.DataAnnotations;

namespace Qick.Dto.Requests
{
    public class RegisterRequest
    {
        // Email
        [Required(ErrorMessage = "Can't be NULL")]
        public string Email { get; set; }

        // Name
        [Required(ErrorMessage = "Can't be NULL")]
        public string Name { get; set; }

        // Password
        [Required(ErrorMessage = "Can't be NULL")]
        public string Password { get; set; }
    }
}
