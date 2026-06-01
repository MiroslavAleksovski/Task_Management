namespace Infrastructure.ExceptionExtensions
{
    public class CustomArgumentNullException : CustomException
    {
        public CustomArgumentNullException(string message) : base(message)
        {
        }   
    }
}
