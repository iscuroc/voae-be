namespace Application.Contracts
{
    public interface IUserNotificationService
    {
        Task SendConfirmationInstructionsAsync(
            string to, 
            string token, 
            CancellationToken cancellationToken = default);
        Task SendResetPasswordInstructionsAsync(
            string to, 
            string token, 
            CancellationToken cancellationToken = default);
    }
}
