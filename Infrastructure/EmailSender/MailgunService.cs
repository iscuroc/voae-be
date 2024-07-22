using Application.Contracts;
using Microsoft.Extensions.Configuration;
using RestSharp;
using RestSharp.Authenticators;

namespace Infrastructure.EmailSender;

public class MailgunService(IConfiguration configuration) : IEmailSender
{
    public async Task SendEmailAsync(string to, string subject, string text, CancellationToken cancellationToken = default)
    {
        string apiKey = configuration["Mailgun:ApiKey"]!;
        string domain = configuration["Mailgun:Domain"]!;
        string from = configuration["Mailgun:From"]!;

        var options = new RestClientOptions
        {
            BaseUrl = new Uri("https://api.mailgun.net/v3"),
            Authenticator = new HttpBasicAuthenticator("api", apiKey)
        };

        var client = new RestClient(options);
        var request = new RestRequest
        {
            Resource = "{domain}/messages",
            Method = Method.Post
        };

        request.AddParameter("domain", domain, ParameterType.UrlSegment);
        request.AddParameter("from", from);
        request.AddParameter("to", to);
        request.AddParameter("subject", subject);
        request.AddParameter("html", text);

        await client.ExecuteAsync(request, cancellationToken);
    }
}