using System.ComponentModel.DataAnnotations;

namespace Qick.Dto.Requests
{
    public class LoginRequest
    {
        // email
        public string Email { get; set; }

        // password
        [Required(ErrorMessage = "Can't be NULL")]
        public string Password { get; set; }
    }
}
