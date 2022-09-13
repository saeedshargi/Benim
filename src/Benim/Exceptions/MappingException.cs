namespace Benim.Exceptions;

public class MappingException: BusinessApplicationException
{
    public MappingException(string message) : base("Mapping Failure", message)
    {
    }
}