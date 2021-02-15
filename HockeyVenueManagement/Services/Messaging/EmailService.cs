using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace HockeyVenueManagement.Services.Messaging
{
    public class EmailService : IMessagingService
    {
        private readonly ILogger<EmailService> _logger;

        public EmailService(ILogger<EmailService> logger)
        {
            _logger = logger;
        }

        public Task SendAsync(string message, string userId)
        {
            // imagine sending logic via an external service

            _logger.LogInformation($"Sending email to user '{userId}'.");
            
            return Task.CompletedTask;
        }
    }
}
