﻿using Application.Contracts;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Mailers;

public class UserMailer(IEmailSender emailSender, IConfiguration configuration) : IUserMailer
{
    private const string FrontendtUrl = "Frontend:Url";
    private const string ConfirmAccountUrl = "UserMailer:ConfirmAccountUrl";
    private const string ResetPasswordUrl = "UserMailer:ResetPasswordUrl";
    private const string ActivityRequestedUrl = "UserMailer:ActivityRequestedUrl";

    public async Task SendConfirmationInstructionsAsync(
        string to,
        string token,
        CancellationToken cancellationToken = default
    )
    {
        const string subject = "Portal Curoc - Confirme su correo electrónico";
        var href = configuration[FrontendtUrl] + configuration[ConfirmAccountUrl]!.Replace("{0}", token);
        var html = $"""
                        <h1>Portal Curoc</h1>
                        <p>Por favor, confirme su correo electrónico haciendo clic en el siguiente enlace:</p>
                        <a href='{href}'>Confirmar correo electrónico</a>
                    """;

        await emailSender.SendEmailAsync(to, subject, html, cancellationToken);
    }

    public async Task SendForgotPasswordInstructionsAsync(
        string to,
        string token,
        CancellationToken cancellationToken = default
        
    )
    {
        const string subject = "Portal Curoc - Restablezca su contraseña";
        var href = configuration[FrontendtUrl] + configuration[ResetPasswordUrl]!.Replace("{0}", token);
        var html = $"""
                        <h1>Portal VOAE</h1>
                        <p>Por favor, restablezca su contraseña haciendo clic en el siguiente enlace:</p>
                        <a href='{href}'>Restablecer contraseña</a>
                    """;

        await emailSender.SendEmailAsync(to, subject, html, cancellationToken);
    }

    public async Task SendResetPasswordConfirmationAsync(
        string to,
        CancellationToken cancellationToken = default
    )
    {
        const string subject = "Portal Curoc - Su contraseña ha sido restablecida";
        const string html = """
                                <h1>Portal Curoc</h1>
                                <p>Su contraseña ha sido restablecida exitosamente.
                                Si usted no realizó esta solicitud, por favor contacte con nuestro soporte.
                                </p>
                            """;

        await emailSender.SendEmailAsync(to, subject, html, cancellationToken);
    }

    public async Task SendWelcomeEmailAsync(string to, CancellationToken cancellationToken = default)
    {
        const string subject = "Bienvenido a Portal Curoc";
        var href = configuration[FrontendtUrl]!;
        var html = $"""
                        <h1>Bienvenido a Portal CUROC</h1>
                        <p>Gracias por registrarte. Estamos encantados de tenerte con nosotros.</p>
                        <p>Explora nuestras funciones y servicios en el siguiente enlace:</p>
                        <a href='{href}'>Ir a Portal CUROC</a>
                    """;

        await emailSender.SendEmailAsync(to, subject, html, cancellationToken);
    }

    public async Task SendActivityRequestedAsync(string to, string activitySlug,
        CancellationToken cancellationToken = default)
    {
        const string subject = "Nueva Actividad en Portal CUROC";
        var href = configuration[FrontendtUrl] + configuration[ActivityRequestedUrl]!.Replace("{0}", activitySlug);
        var html = $"""
                        <h1>Se ha solicitado una nueva actividad</h1>
                        <p>Hay una nueva actividad disponible para ti. Puedes verla en el siguiente enlace:</p>
                        <a href='{href}'>Ver Actividad</a>
                    """;

        await emailSender.SendEmailAsync(to, subject, html, cancellationToken);
    }

    public async Task SendActivityApprovedAsync(string to, string activityName, CancellationToken cancellationToken = default)
{
    const string subject = "Actividad Aprobada en Portal CUROC";
    var html = $"""
                    <h1>Tu actividad ha sido aprobada</h1>
                    <p>La actividad "{activityName}" ha sido aprobada y ya está disponible en el portal.</p>
                """;

    await emailSender.SendEmailAsync(to, subject, html, cancellationToken);
}

    public Task SendActivityPublishedAsync(string to, string activityName, CancellationToken cancellationToken = default)
    {
        const string subject = "Actividad Publicada en Portal CUROC";
        var html = $"""
                    <h1>Actividad Publicada</h1>
                    <p>La actividad "{activityName}" ha sido publicada y ya está disponible en el portal.</p>
                """;

        return emailSender.SendEmailAsync(to, subject, html, cancellationToken);
    }


    public async Task SendActivityRejectAsync(string to,string activityName, List<string> reviewerObservations, CancellationToken cancellationToken = default)
    {
        const string subject = "Actividad Rechazada en Portal CUROC";

        var observationsList = string.Join("<br>", reviewerObservations);

        var html = $"""
                    <h1>Actividad Rechazada</h1>
                    <p>La actividad "{activityName}" ha sido rechazada.</p>
                    <p>Observaciones:</p>
                    <ul>
                        <p>{observationsList}</p>
                    </ul>
                """;

        await emailSender.SendEmailAsync(to, subject, html, cancellationToken);
    }



}