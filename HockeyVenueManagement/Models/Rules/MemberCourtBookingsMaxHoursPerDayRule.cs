using HockeyVenueManagement.Services;
using System.Threading.Tasks;


namespace HockeyVenueManagement.Models.Rules
{
    public class MemberPitchBookingsMaxHoursPerDayRule : IPitchBookingRule
    {
        private readonly IPitchBookingService _pitchBookingService;

        public MemberPitchBookingsMaxHoursPerDayRule(IPitchBookingService pitchBookingService)
        {
            _pitchBookingService = pitchBookingService;
        }

        public async Task<bool> CompliesWithRuleAsync(PitchBooking booking)
        {
            var hoursBooked = await _pitchBookingService.GetBookedHoursForMemberAsync(booking.Member, booking.StartDateTime.Date);

            var hoursRequested = (booking.EndDateTime - booking.StartDateTime).Hours;

            return hoursBooked + hoursRequested <= 5;
        }

        public string ErrorMessage => "Members may only book a total of 5 hours of pitch time per day.";
    }
}
