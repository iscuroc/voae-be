using Shared;

namespace Domain.Errors;

public class OrganizationErrors
{
    public static Error OrganizationNotFound => Error.NotFound(
        "Career.OrganizationNotFound",
        "Organization not found"
    );
}