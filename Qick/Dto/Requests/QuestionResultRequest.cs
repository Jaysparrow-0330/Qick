namespace Qick.Dto.Requests
{
    public class QuestionResultRequest
    {
        public int Id { get; set; }
        public string value { get; set; }
        public ICollection<AnswerResultRequest> Answers { get; set; }
    }
}
