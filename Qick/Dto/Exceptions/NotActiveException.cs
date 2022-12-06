namespace Qick.Dto.Exceptions
{
    public class NotActiveException : Exception
    {
        public NotActiveException()
        {
        }

        public NotActiveException(string message) : base(message)
        {
        }

        public NotActiveException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
