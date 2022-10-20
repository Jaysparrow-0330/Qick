namespace Qick.Dto.Requests
{
    public class CalculateResultRequest
    {
        public int Id { get; set; }
        public ICollection<QuestionResultRequest> questions { get; set; }
    }
}
