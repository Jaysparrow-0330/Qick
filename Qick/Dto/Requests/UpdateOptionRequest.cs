namespace Qick.Dto.Requests
{
    public class UpdateOptionRequest
    {
        public int Id { get; set; }
        public string OptionContent { get; set; }
        public string Value { get; set; }
        public string? Status { get; set; }
    }
}
