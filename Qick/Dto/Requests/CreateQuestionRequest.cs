namespace Qick.Dto.Requests
{
    public class CreateQuestionRequest
    {
        public int? TestId { get; set; }
        public string? QuestionContent { get; set; }
        public int? QuestionTypeId { get; set; }
        public string? Value { get; set; }
        public ICollection<CreateOptionRequest> Options { get; set; }
    }
}
