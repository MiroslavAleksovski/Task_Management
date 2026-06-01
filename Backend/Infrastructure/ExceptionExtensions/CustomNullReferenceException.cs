namespace Infrastructure.ExceptionExtensions
{
    public class CustomNullReferenceException : CustomException
    {
        public CustomNullReferenceException(string message) : base(message)
        {
        }   
    }
}
