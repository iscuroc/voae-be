namespace Application.Contracts;

public interface IUserMailer
{
    Task SendConfirmationInstructionsAsync(
        string to, 
        string token,
        CancellationToken cancellationToken = default
    );
    
    Task SendForgotPasswordInstructionsAsync(
        string to, 
        string token, 
        CancellationToken cancellationToken = default
    );
    
    Task SendWelcomeEmailAsync(
        string to,
        CancellationToken cancellationToken = default
    );

    Task SendResetPasswordConfirmationAsync(
        string to,
        CancellationToken cancellationToken = default
    );
    
    Task SendActivityRequestedAsync(
        string to, 
        string activitySlug, 
        CancellationToken cancellationToken = default
    );

    Task SendActivityApprovedAsync(
        string to,
        string activityName,
        CancellationToken cancellationToken = default
    );
    
    Task SendActivityPublishedAsync(
        string to,
        string activityName,
        CancellationToken cancellationToken = default
    );

    Task SendActivityRejectAsync(
        string to,
        string activityName,
        List<string> reviewerObservations,
        CancellationToken cancellationToken = default
    );
}
