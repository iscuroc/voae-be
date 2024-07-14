using Application.Contracts;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Services;

public class UserNotificationService(
    IEmailSender emailSender,
    IConfiguration configuration
) : IUserNotificationService
{
    public async Task SendConfirmationInstructionsAsync(
        string to,
        string token,
        CancellationToken cancellationToken = default
    )
    {
        var subject = "Portal Curoc - Confirme su correo electrónico";
        var host = configuration["Frontend:Url"]!;
        var html = $@"
            <h1>Portal Curoc</h1>
            <p>Por favor, confirme su correo electrónico haciendo clic en el siguiente enlace:</p>
            <a href='{host}/confirm-email?token={token}'>Confirmar correo electrónico</a>";

        await emailSender.SendEmailAsync(to, subject, html);
    }

    public async Task SendResetPasswordInstructionsAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}