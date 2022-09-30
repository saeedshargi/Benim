using Benim.Domain.Common;

namespace Benim.Domain.ValueObjects;

public class Error : ValueObject
{
    public static readonly Error None = new(string.Empty, string.Empty);
    public static readonly Error NullValue = new("Error.NullValue", "The specified result value is null.");

    public Error(string code, string message)
    {
        Code = code;
        Message = message;
    }

    public string Code { get; }
    public string Message { get; }

    public static implicit operator string(Error error) => error.Code;

    protected override IEnumerable<object> GetValues()
    {
        yield return Code;
        yield return Message;
    }
}