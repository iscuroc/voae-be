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

    public async Task SendResetPasswordInstructionsAsync(
        string to,
        string token,
        CancellationToken cancellationToken = default
    )
    {
        var subject = "Portal Curoc - Restablezca su contraseña";
        var host = configuration["Frontend:Url"]!;
        var html = $@"
            <h1>Portal Curoc</h1>
            <p>Por favor, restablezca su contraseña haciendo clic en el siguiente enlace:</p>
            <a href='{host}/reset-password?token={token}'>Restablecer contraseña</a>";

        await emailSender.SendEmailAsync(to, subject, html);
    }
    
    public async Task SendPasswordResetConfirmationAsync(
        string to,
        CancellationToken cancellationToken = default
    )
    {
        var subject = "Portal Curoc - Su contraseña ha sido restablecida";
        var html = $@"
            <h1>Portal Curoc</h1>
            <p>Su contraseña ha sido restablecida exitosamente. Si usted no realizó esta solicitud, por favor contacte con nuestro soporte.</p>";

        await emailSender.SendEmailAsync(to, subject, html);
    }
    
    
    public async Task SendWelcomeEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
