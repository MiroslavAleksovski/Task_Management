namespace Infrastructure.ExceptionExtensions
{
    public class CustomException : Exception
    {
        public CustomException(string message) : base(message)
        {
        }
    }
}
