using System.ComponentModel.DataAnnotations;

namespace Qick.Controllers.Requests
{
    public class LoginRequest
    {
        /// <summary>
        /// email
        /// </summary>
        [Required(ErrorMessage = "Can't be NULL"), EmailAddress(ErrorMessage = "Wrong email format")]
        public string Email { get; set; }

        /// <summary>
        /// passwordrtyertyertyeryerty
        ///
        /// </summary>
        [Required(ErrorMessage = "Can't be NULL")]
        public string Password { get; set; }
    }
}
