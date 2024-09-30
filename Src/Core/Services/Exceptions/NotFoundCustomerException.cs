namespace Core.Services.Exceptions;

public class InvalidCustomerJwtException : Exception
{
    const string MESSAGE_TEMPLATE = "JWT is Invalid"; 
    public InvalidCustomerJwtException() : base(MESSAGE_TEMPLATE)
    {
        
    }
}
