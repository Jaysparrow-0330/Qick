namespace Qick.Dto.Requests
{
    public class QuestionRequest
    {
        public int TestId { get; set; }
        public int? QuestionId { get; set; }
        public string? QuestionContent { get; set; }
        public int? QuestionTypeId { get; set; }
        public string? Value { get; set; }
        public string? Status { get; set; }
        public ICollection<OptionRequest> Options { get; set; }
    }
}
