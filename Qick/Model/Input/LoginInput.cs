using System.ComponentModel.DataAnnotations;
namespace Qick.Model.Input
{
    public class LoginInput
    {
        /// <summary>
        /// email
        /// </summary>
        [Required(ErrorMessage = "Can't be NULL"), EmailAddress(ErrorMessage = "Wrong email format")]
        public string Email { get; set; }

        /// <summary>
        /// password
        /// </summary>
        [Required(ErrorMessage = "Can't be NULL")]
        public string Password { get; set; }
    }
}
