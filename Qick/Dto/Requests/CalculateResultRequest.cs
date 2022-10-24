namespace Qick.Dto.Requests
{
    public class CalculateResultRequest
    {
        public int TestId { get; set; }
        public ICollection<QuestionResultRequest> questions { get; set; }
    }
}
