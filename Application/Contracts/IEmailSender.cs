namespace Application.Contracts;

public interface IEmailSender
{
    Task SendEmailAsync(string to, string subject, string text);
}