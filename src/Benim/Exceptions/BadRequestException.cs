namespace Benim.Exceptions;

public class BadRequestException: BusinessApplicationException
{
    public BadRequestException(string message) : base("Bad Request", message)
    {
    }
}