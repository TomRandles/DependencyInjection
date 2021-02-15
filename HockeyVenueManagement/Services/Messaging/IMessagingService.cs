using System.Threading.Tasks;

namespace HockeyVenueManagement.Services.Messaging
{
    public interface IMessagingService
    {
        Task SendAsync(string message, string userId);
    }
}