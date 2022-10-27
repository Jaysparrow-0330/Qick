namespace Qick.Dto.Requests
{
    public class UpdateListQuestionRequest
    {
        public int TestId { get; set; }
        public ICollection<UpdateQuestionRequest> questions { get; set; }
    }
}
