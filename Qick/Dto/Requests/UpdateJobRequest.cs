using Qick.Models;

namespace Qick.Dto.Requests
{
    public class UpdateJobRequest
    {
        public int Id { get; set; }
        public string? JobName { get; set; }
        public string? Status { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }

        public virtual ICollection<JobMajorRequest> JobMajors { get; set; }
    }
}
