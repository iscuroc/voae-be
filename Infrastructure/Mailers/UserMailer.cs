using Application.Contracts;
using Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Mailers
{
    public class UserMailer : IUserMailer
    {
        private readonly EmailBackgroundService _emailBackgroundService;
        private readonly IEmailSender _emailSender;
        private readonly IConfiguration _configuration;

        private const string FrontendUrlKey = "Frontend:Url";
        private const string ConfirmAccountUrlKey = "UserMailer:ConfirmAccountUrl";
        private const string ResetPasswordUrlKey = "UserMailer:ResetPasswordUrl";
        private const string ActivityRequestedUrlKey = "UserMailer:ActivityRequestedUrl";

        public UserMailer(IEmailSender emailSender, IConfiguration configuration, EmailBackgroundService emailBackgroundService)
        {
            _emailSender = emailSender;
            _configuration = configuration;
            _emailBackgroundService = emailBackgroundService;
        }

        public async Task SendConfirmationInstructionsAsync(
            string to,
            string token,
            CancellationToken cancellationToken = default
        )
        {
            const string subject = "Portal Curoc - Confirme su correo electrónico";
            var href = _configuration[FrontendUrlKey] + _configuration[ConfirmAccountUrlKey]!.Replace("{0}", token);
            var html = $"""
                        <h1>Portal Curoc</h1>
                        <p>Por favor, confirme su correo electrónico haciendo clic en el siguiente enlace:</p>
                        <a href='{href}'>Confirmar correo electrónico</a>
                    """;

            _emailBackgroundService.QueueEmail(() => _emailSender.SendEmailAsync(to, subject, html, cancellationToken));
            //await _emailSender.SendEmailAsync(to, subject, html, cancellationToken);
        }

        public async Task SendForgotPasswordInstructionsAsync(
            string to,
            string token,
            CancellationToken cancellationToken = default
        )
        {
            const string subject = "Portal Curoc - Restablezca su contraseña";
            var href = _configuration[FrontendUrlKey] + _configuration[ResetPasswordUrlKey]!.Replace("{0}", token);
            var html = $"""
                        <h1>Portal Curoc</h1>
                        <p>Por favor, restablezca su contraseña haciendo clic en el siguiente enlace:</p>
                        <a href='{href}'>Restablecer contraseña</a>
                    """;

            _emailBackgroundService.QueueEmail(() => _emailSender.SendEmailAsync(to, subject, html, cancellationToken));
            // await _emailSender.SendEmailAsync(to, subject, html, cancellationToken);
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

            _emailBackgroundService.QueueEmail(() => _emailSender.SendEmailAsync(to, subject, html, cancellationToken));
            //await _emailSender.SendEmailAsync(to, subject, html, cancellationToken);
        }

        public async Task SendWelcomeEmailAsync(string to, CancellationToken cancellationToken = default)
        {
            const string subject = "Bienvenido a Portal Curoc";
            var href = _configuration[FrontendUrlKey]!;
            var html = $"""
                        <h1>Bienvenido a Portal CUROC</h1>
                        <p>Gracias por registrarte. Estamos encantados de tenerte con nosotros.</p>
                        <p>Explora nuestras funciones y servicios en el siguiente enlace:</p>
                        <a href='{href}'>Ir a Portal CUROC</a>
                    """;

            _emailBackgroundService.QueueEmail(() => _emailSender.SendEmailAsync(to, subject, html, cancellationToken));
            //await _emailSender.SendEmailAsync(to, subject, html, cancellationToken);
        }

        public async Task SendActivityRequestedAsync(string to, string activitySlug,
            CancellationToken cancellationToken = default)
        {
            const string subject = "Nueva Actividad en Portal CUROC";
            var href = _configuration[FrontendUrlKey] + _configuration[ActivityRequestedUrlKey]!.Replace("{0}", activitySlug);
            var html = $"""
                        <h1>Se ha solicitado una nueva actividad</h1>
                        <p>Hay una nueva actividad disponible para ti. Puedes verla en el siguiente enlace:</p>
                        <a href='{href}'>Ver Actividad</a>
                    """;

            _emailBackgroundService.QueueEmail(() => _emailSender.SendEmailAsync(to, subject, html, cancellationToken));
            //await _emailSender.SendEmailAsync(to, subject, html, cancellationToken);
        }

        public async Task SendActivityApprovedAsync(string to, string activityName, CancellationToken cancellationToken = default)
        {
            const string subject = "Actividad Aprobada en Portal CUROC";
            var html = $"""
                        <h1>Tu actividad ha sido aprobada</h1>
                        <p>La actividad "{activityName}" ha sido aprobada y ya está disponible en el portal.</p>
                    """;

            _emailBackgroundService.QueueEmail(() => _emailSender.SendEmailAsync(to, subject, html, cancellationToken));
            //await _emailSender.SendEmailAsync(to, subject, html, cancellationToken);
        }

        public Task SendActivityPublishedAsync(string to, string activityName, CancellationToken cancellationToken = default)
        {
            const string subject = "Actividad Publicada en Portal CUROC";
            var html = $"""
                        <h1>Actividad Publicada</h1>
                        <p>La actividad "{activityName}" ha sido publicada y ya está disponible en el portal.</p>
                    """;

            _emailBackgroundService.QueueEmail(() => _emailSender.SendEmailAsync(to, subject, html, cancellationToken));
            return Task.CompletedTask;
            // return _emailSender.SendEmailAsync(to, subject, html, cancellationToken);
        }

        public async Task SendActivityRejectAsync(string to, string activityName, List<string> reviewerObservations, CancellationToken cancellationToken = default)
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

            _emailBackgroundService.QueueEmail(() => _emailSender.SendEmailAsync(to, subject, html, cancellationToken));
            //await _emailSender.SendEmailAsync(to, subject, html, cancellationToken);
        }
    }
}
