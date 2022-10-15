using System.ComponentModel.DataAnnotations;

namespace Qick.Controllers.Requests
{
    public class RegisterRequest
    {
        [Required(ErrorMessage = "Can't be NULL"), EmailAddress(ErrorMessage = "Wrong email format")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Can't be NULL")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Can't be NULL")]
        public string Password { get; set; }
    }
}
