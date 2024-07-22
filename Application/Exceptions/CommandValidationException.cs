using FluentValidation.Results;

namespace Application.Exceptions;

public class CommandValidationException<TRequest>(
    IReadOnlyCollection<ValidationFailure> validationFailures
) : CommandValidationException(
    validationFailures,
    $"Validación de {typeof(TRequest)} falló por las siguientes razones: {Reasons(validationFailures)}"
);

public class CommandValidationException : Exception
{
    public List<string> Errors { get; }

    protected CommandValidationException(
        IReadOnlyCollection<ValidationFailure> validationFailures,
        string message = ""
    ) : base(string.IsNullOrWhiteSpace(message)
        ? $"Validación falló debido a las siguientes razones: {Reasons(validationFailures)}"
        : message)
    {
        Errors = validationFailures.Select(x => x.ErrorMessage).ToList();
    }

    protected static string Reasons(IEnumerable<ValidationFailure> validationFailures)
    {
        return string.Join(", ", validationFailures.Select(x => $"{x.PropertyName}: {x.ErrorMessage}"));
    }
}