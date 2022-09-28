namespace Qick.Controllers.Responses
{
    public class HttpStatusCodeResponse
    {
        public int Code { get; set; }
        public string? Message { get; set; }

        public HttpStatusCodeResponse(int code, string message)
        {
            Code = code;
            Message = message;
        }

        public HttpStatusCodeResponse(int code)
        {
            Code = code;
            switch (code)
            {
                case 200:
                    Message = "Success";
                    break;

                case 210:
                    Message = "Not Active";
                    break;

                case 204:
                    Message = "Nothing Change";
                    break;

                case 400:
                    Message = "Error";
                    break;

                case 410:
                    Message = "Email Used";
                    break;

                default:
                    Code = 400;
                    Message = "Error";
                    break;
            }
        }
    }
}
