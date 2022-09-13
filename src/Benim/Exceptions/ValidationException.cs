namespace Benim.Exceptions;

public class ValidationException: BusinessApplicationException
{
    public ValidationException(IReadOnlyDictionary<string, string[]> errors) : base("Validation Failure", "One or more validation errors occurred")
    {
        Errors = errors;
    }

    public IReadOnlyDictionary<string, string[]> Errors { get; }
}