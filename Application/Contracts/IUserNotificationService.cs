namespace Application.Contracts;

public interface IUserNotificationService
{
    Task SendConfirmationInstructionsAsync(CancellationToken cancellationToken = default);
    Task SendResetPasswordInstructionsAsync(CancellationToken cancellationToken = default);
}