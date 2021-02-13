using System.Threading.Tasks;

namespace HockeyVenueManagement.Services.Notification
{
    public interface INotificationService
    {
        Task SendAsync(string message, string userId);
    }
}