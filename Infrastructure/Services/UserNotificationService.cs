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

    public async Task SendResetPasswordInstructionsAsync(
        string to,
        string token,
        CancellationToken cancellationToken = default
    )
    {
        const string subject = "Portal Curoc - Restablezca su contraseña";
        var host = configuration["Frontend:Url"]!;
        var html = $"""
                        <h1>Portal Curoc</h1>
                        <p>Por favor, restablezca su contraseña haciendo clic en el siguiente enlace:</p>
                        <a href='{host}/reset-password?token={token}'>Restablecer contraseña</a>
                    """;

        await emailSender.SendEmailAsync(to, subject, html, cancellationToken);
    }
    
    public async Task SendPasswordResetConfirmationAsync(
        string to,
        CancellationToken cancellationToken = default
    )
    {
        const string subject = "Portal Curoc - Su contraseña ha sido restablecida";
        const string html = """
                                <h1>Portal Curoc</h1>
                                <p>Su contraseña ha sido restablecida exitosamente. Si usted no realizó esta solicitud, por favor contacte con nuestro soporte.</p>
                            """;

        await emailSender.SendEmailAsync(to, subject, html, cancellationToken);
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
    
    public async Task SendNewActivityEmailAsync(string to, int activityLink, CancellationToken cancellationToken = default)
    {
        const string subject = "Nueva Actividad en Portal CUROC";
        var host = configuration["Frontend:Url"+ activityLink]!;
        var html = $"""
                        <h1>Se ha creado una nueva actividad</h1>
                        <p>Hay una nueva actividad disponible para ti. Puedes verla en el siguiente enlace:</p>
                        <a href='{host}'>Ver Actividad</a>
                    """;

        await emailSender.SendEmailAsync(to, subject, html, cancellationToken);
    }
}
