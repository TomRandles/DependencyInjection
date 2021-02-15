using System.Collections.Generic;
using System.Threading.Tasks;

namespace HockeyVenueManagement.Services.Messaging
{
    public class CompositeMessagingService : IMessagingService
    {
        private readonly IEnumerable<IMessagingService> _messagingServices;

        public CompositeMessagingService(IEnumerable<IMessagingService> messagingServices)
        {
            _messagingServices = messagingServices;
        }

        public async Task SendAsync(string message, string userId)
        {
            foreach (var messagingService in _messagingServices)
            {
                await messagingService.SendAsync(message, userId);
            }
        }
    }
}