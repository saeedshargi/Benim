using Benim.Features.Shared;
using FluentValidation;
using MediatR;
using ValidationException = Benim.Exceptions.ValidationException;

namespace Benim.Behaviors;

public class ValidationBehavior<TRequest,TResponse>: IPipelineBehavior<TRequest,TResponse> where TRequest : class,  ICommand<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        if (!_validators.Any())
        {
            return await next();
        }

        ValidationContext<TRequest> context = new ValidationContext<TRequest>(request);

        Dictionary<string, string[]> errors = _validators
            .Select(x => x.Validate(context))
            .SelectMany(x => x.Errors)
            .Where(x => x != null)
            .GroupBy(x => x.PropertyName, x => x.ErrorMessage,
                (propertyName, errorMessages) => new
                {
                    Key = propertyName,
                    Values = errorMessages.Distinct().ToArray()
                })
            .ToDictionary(x => x.Key, x => x.Values);

        if (errors.Any())
        {
            throw new ValidationException(errors);
        }

        return await next();
    }
}