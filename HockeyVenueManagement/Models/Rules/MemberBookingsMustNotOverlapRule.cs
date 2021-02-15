using HockeyVenueManagement.Models;
using HockeyVenueManagement.Models.Rules;
using HockeyVenueManagement.Services;
using System.Linq;
using System.Threading.Tasks;

namespace TennisBookings.Web.Domain.Rules
{
    public class MemberBookingsMustNotOverlapRule : IPitchBookingRule
    {
        private readonly IPitchBookingService _pitchBookingService;

        public MemberBookingsMustNotOverlapRule(IPitchBookingService pitchBookingService)
        {
            _pitchBookingService = pitchBookingService;
        }

        public string ErrorMessage => "You cannot make two bookings which overlap";

        public async Task<bool> CompliesWithRuleAsync(PitchBooking booking)
        {
            var todaysBookings = (await _pitchBookingService
                .MemberBookingsForDayAsync(booking.StartDateTime.Date, booking.Member))
                .ToArray();

            if (!todaysBookings.Any())
                return true; // no bookings, so cannot be overlap

            var bookingHours = Enumerable.Range(booking.StartDateTime.Hour,
                booking.EndDateTime.Hour - booking.StartDateTime.Hour).ToArray();

            foreach (var existingBooking in todaysBookings)
            {
                for (var hour = existingBooking.StartDateTime.Hour; hour < existingBooking.EndDateTime.Hour; hour++)
                {
                    if (bookingHours.Contains(hour))
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
