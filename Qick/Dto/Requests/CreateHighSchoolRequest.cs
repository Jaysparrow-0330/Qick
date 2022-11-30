namespace Qick.Dto.Requests
{
    public class CreateHighSchoolRequest
    {
        public string? HighSchoolName { get; set; }
        public string? HighSchoolCode { get; set; }
        public string? HighSchoolAddress { get; set; }
        public int? WardId { get; set; }
    }
}
