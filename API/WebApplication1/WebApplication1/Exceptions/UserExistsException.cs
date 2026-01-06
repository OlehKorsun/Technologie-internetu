namespace WebApplication1.Exceptions;

public class UserExistsException : Exception
{
    public UserExistsException(string message) : base(message){}
}