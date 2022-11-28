namespace Qick.Dto.Responses
{
    public class ResultResponse
    {
        public Guid Id { get; set; }
        public string? ResultName { get; set; }
        public string? ResultSummary { get; set; }
        public string? ResultRelationship { get; set; }
        public string? ResultSuccessRule { get; set; }
        public string? ResultShortName { get; set; }
        public string? ResultPictureUrl { get; set; }
        public string? ResultCareer { get; set; }
    }

}
