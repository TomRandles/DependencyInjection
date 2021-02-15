using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace HockeyVenueManagement.Services.Messaging
{
    public class SMSService : IMessagingService
    {
        private readonly ILogger<SMSService> _logger;

        public SMSService(ILogger<SMSService> logger)
        {
            _logger = logger;
        }

        public Task SendAsync(string message, string userId)
        {
            // imagine sending logic via an external service

            _logger.LogInformation($"Sending SMS to user '{userId}'.");

            return Task.CompletedTask;
        }
    }
}