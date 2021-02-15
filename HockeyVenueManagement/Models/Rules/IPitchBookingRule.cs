using System.Threading.Tasks;


namespace HockeyVenueManagement.Models.Rules
{
    public interface IPitchBookingRule
    {
        Task<bool> CompliesWithRuleAsync(PitchBooking booking);

        string ErrorMessage { get; }
    }
}
