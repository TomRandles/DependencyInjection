using HockeyVenueManagement.Models;
using HockeyVenueManagement.Models.Configuration;
using HockeyVenueManagement.Models.Rules;
using System.Threading.Tasks;


namespace TennisBookings.Web.Domain.Rules
{
    public class ClubIsOpenRule : IPitchBookingRule
    {
        private readonly IClubConfiguration _clubConfiguration;

        public ClubIsOpenRule(IClubConfiguration clubConfiguration)
        {
            _clubConfiguration = clubConfiguration;
        }

        public Task<bool> CompliesWithRuleAsync(PitchBooking booking)
        {
            var startHourPasses = booking.StartDateTime.Hour >= _clubConfiguration.OpenHour;
            var endHourPasses = booking.EndDateTime.Hour <= _clubConfiguration.CloseHour;

            return Task.FromResult(startHourPasses && endHourPasses);
        }

        public string ErrorMessage => "Can't make a booking when the club is closed";
    }
}
