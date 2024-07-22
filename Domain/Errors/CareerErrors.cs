using Shared;

namespace Domain.Errors;

public class CareerErrors
{
    public static Error CareerNotFound => Error.NotFound(
        "Career.CareerNotFound",
        "Career not found"
    );
}