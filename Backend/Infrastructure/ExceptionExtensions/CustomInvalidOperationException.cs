namespace Infrastructure.ExceptionExtensions
{
    public class CustomInvalidOperationException : CustomException
    {
        public CustomInvalidOperationException(string message) : base(message)
        {
        }   
    }
}
