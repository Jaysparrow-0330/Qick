namespace Qick.Dto.Requests
{
    public class CreateTestStepTwoRequest
    {
        public int TotalQuestion { get; set; }
        public ICollection<QuestionRequest> Questions { get; set; }
    }
}
