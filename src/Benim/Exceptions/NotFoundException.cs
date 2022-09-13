namespace Benim.Exceptions;

public class NotFoundException: BusinessApplicationException
{
    public NotFoundException(string message) : base("Not Found", message)
    {
    }
}