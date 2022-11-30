namespace Qick.Dto.Responses
{
    public class ListHighSchoolResponse
    {
        public Guid Id { get; set; }
        public string? HighSchoolName { get; set; }
        public string? HighSchoolCode { get; set; }
        public string? Status { get; set; }
        public string? HighSchoolAddress { get; set; }
        public int? WardId { get; set; }
    }
}
