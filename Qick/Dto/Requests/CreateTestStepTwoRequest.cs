namespace Qick.Dto.Requests
{
    public class CreateTestStepTwoRequest
    {
        public ICollection<CreateQuestionRequest> Questions { get; set; }
    }
}
