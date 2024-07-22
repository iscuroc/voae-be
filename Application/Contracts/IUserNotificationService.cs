namespace Application.Contracts;

public interface IUserNotificationService
{
    Task SendConfirmationInstructionsAsync(
        string to, 
        string token,
        CancellationToken cancellationToken = default
    );
    
    Task SendResetPasswordInstructionsAsync(
        string to, 
        string token, 
        CancellationToken cancellationToken = default
    );
    
    Task SendWelcomeEmailAsync(
        string email, 
        CancellationToken cancellationToken = default
    
    );

    Task SendPasswordResetConfirmationAsync(
        string to,
        CancellationToken cancellationToken = default
    );
}
