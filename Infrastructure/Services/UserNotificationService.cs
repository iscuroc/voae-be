using Application.Contracts;

namespace Infrastructure.Services;

public class UserNotificationService : IUserNotificationService
{
    public async Task SendConfirmationInstructionsAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task SendResetPasswordInstructionsAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}