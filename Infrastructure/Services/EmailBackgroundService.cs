using Microsoft.Extensions.Hosting;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services
{
    public class EmailBackgroundService : BackgroundService
    {
        private readonly ConcurrentQueue<Func<Task>> _emailQueue = new();
        private readonly ILogger<EmailBackgroundService> _logger;

        public EmailBackgroundService(ILogger<EmailBackgroundService> logger)
        {
            _logger = logger;
        }

        public void QueueEmail(Func<Task> emailTask)
        {
            _emailQueue.Enqueue(emailTask);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                if (_emailQueue.TryDequeue(out var emailTask))
                {
                    try
                    {
                        await emailTask();
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error enviando correo");
                    }
                }

                await Task.Delay(500, stoppingToken);
            }
        }
    }
}
