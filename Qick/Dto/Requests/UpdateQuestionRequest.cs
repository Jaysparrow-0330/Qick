namespace Qick.Dto.Requests
{
    public class UpdateQuestionRequest
    {
        public int Id { get; set; }
        public int? TestId { get; set; }
        public string? QuestionContent { get; set; }
        public int? QuestionTypeId { get; set; }
        public string? Status { get; set; }
        public string? Value { get; set; }
        public List<UpdateOptionRequest> Options { get; set; }
    }
}
