namespace Qick.Dto.Responses
{
    public class TakingTestResponse
    {
        public int Id { get; set; }
        public string QuizName { get; set; }
        public ICollection<QuestionResponse> questions { get; set; }
    }
}
