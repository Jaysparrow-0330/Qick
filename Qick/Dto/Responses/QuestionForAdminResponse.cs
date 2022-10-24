namespace Qick.Dto.Responses
{
    public class QuestionForAdminResponse
    {
        public int Id { get; set; }
        public string? QuestionContent { get; set; }
        public int? QuestionTypeId { get; set; }
        public string? Status { get; set; }
        public string? Value { get; set; }
        public List<OptionForAdminResponse> Options { get; set; }
    }
}
