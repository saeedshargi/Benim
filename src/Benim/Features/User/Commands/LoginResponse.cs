namespace Benim.Features.User.Commands;

public class LoginResponse
{
    public LoginResponse()
    {
        Errors = new List<string>();
    }

    public bool Success => Errors.Count == 0;

    public List<string> Errors { get; }

    public void AddError(string errorMessage)
    {
        Errors.Add(errorMessage);
    }

    public override string ToString()
    {
        return Errors.Aggregate("", (current, errorItem) => current + $@"{errorItem}");
    }
}