using Benim.Domain.ValueObjects;

namespace Benim.Domain.Common;

public class Result<TValue> where TValue : class
{
    private readonly TValue? _value;

    private Result(TValue value)
    {
        IsSuccess = true;
        Error = Error.None;
        _value = value;
    }

    private Result(Error error)
    {
        if (error == Error.None)
        {
            throw new InvalidOperationException("The error cant not be empty.");
        }
        IsSuccess = false;
        Error = error;
        _value = default(TValue);
    }

    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public Error Error { get; }
    public TValue? Value => IsSuccess ? _value : null;
    public static Result<TValue> Success(TValue value) => new(value);
    public static Result<TValue> Failure(Error error) => new(error);
}