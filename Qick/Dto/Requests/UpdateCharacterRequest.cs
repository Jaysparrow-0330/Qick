using Qick.Models;

namespace Qick.Dto.Requests
{
    public class UpdateCharacterRequest
    {
        public Guid Id { get; set; }
        public string? ResultName { get; set; }
        public string? ResultSummary { get; set; }
        public string? ResultRelationship { get; set; }
        public string? ResultSuccessRule { get; set; }
        public string? ResultShortName { get; set; }
        public string? ResultPictureUrl { get; set; }
        public string? ResultCareer { get; set; }
        public string? Value { get; set; }

        public virtual ICollection<JobMappingRequest> JobMappings { get; set; }
    }
}
