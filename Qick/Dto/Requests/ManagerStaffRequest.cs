using System.ComponentModel.DataAnnotations;

namespace Qick.Dto.Requests
{
    public class ManagerStaffRequest
    {
        // Email
        [Required(ErrorMessage = "Can't be NULL")]
        public string Email { get; set; }

        // Name
        [Required(ErrorMessage = "Can't be NULL")]
        public string Name { get; set; }

        // University
        [Required(ErrorMessage = "Can't be NULL")]
        public Guid UniversityId { get; set; }

    }
}
