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
        const string subject = "Portal Curoc - Confirme su correo electrónico";
        var host = configuration["Frontend:Url"]!;
        var html = $"""
                        <h1>Portal Curoc</h1>
                        <p>Por favor, confirme su correo electrónico haciendo clic en el siguiente enlace:</p>
                        <a href='{host}/confirm-email?token={token}'>Confirmar correo electrónico</a>
                    """;

        await emailSender.SendEmailAsync(to, subject, html, cancellationToken);
    }

    public async Task SendResetPasswordInstructionsAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task SendWelcomeEmailAsync(string to, CancellationToken cancellationToken = default)
    {
        const string subject = "Bienvenido a Portal Curoc";
        var host = configuration["Frontend:Url"]!;
        var html = $"""
                        <h1>Bienvenido a Portal CUROC</h1>
                        <p>Gracias por registrarte. Estamos encantados de tenerte con nosotros.</p>
                        <p>Explora nuestras funciones y servicios en el siguiente enlace:</p>
                        <a href='{host}'>Ir a Portal CUROC</a>
                    """;

        await emailSender.SendEmailAsync(to, subject, html, cancellationToken);
    }
}