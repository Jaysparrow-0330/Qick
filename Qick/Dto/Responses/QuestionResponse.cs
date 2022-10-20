namespace Qick.Dto.Responses
{
    public class QuestionResponse
    {
        public int Id { get; set; }
        public string? QuestionContent { get; set; }
        public string? Value { get; set; }

        public List<OptionResponse> Options { get; set; }
    }
}
