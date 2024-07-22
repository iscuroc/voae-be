using Application.Exceptions;
using FluentValidation;
using Mediator;

namespace Application.Behaviors;

public sealed class ValidationBehavior<TMessage, TResponse>(
    IValidator<TMessage> validator
) : IPipelineBehavior<TMessage, TResponse> where TMessage : IBaseCommand
{
    public async ValueTask<TResponse> Handle(
        TMessage message,
        CancellationToken cancellationToken,
        MessageHandlerDelegate<TMessage, TResponse> next)
    {
        var context = new ValidationContext<TMessage>(message);

        var validationResult = await validator.ValidateAsync(context, cancellationToken);

        var failures = validationResult.Errors.Where(f => f != null).Distinct().ToList();

        if (failures.Count != 0)
        {
            throw new CommandValidationException<TMessage>(failures);
        }
        
        return await next(message, cancellationToken);
    }
}