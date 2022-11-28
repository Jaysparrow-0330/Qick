namespace Qick.Dto.Requests
{
    public class CreateTestStepTwoRequest
    {
        public int TotalQuestion { get; set; }
        public ICollection<CreateQuestionRequest> Questions { get; set; }
    }
}
