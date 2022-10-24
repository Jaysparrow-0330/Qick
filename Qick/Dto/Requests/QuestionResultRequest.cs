namespace Qick.Dto.Requests
{
    public class QuestionResultRequest
    {
        public int questionId { get; set; }
        public string questionvalue { get; set; }
        public ICollection<AnswerResultRequest> Options { get; set; }
    }
}
