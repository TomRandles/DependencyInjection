using System.Threading.Tasks;
using HockeyVenueManagement.Models;
using HockeyVenueManagement.Models.Configuration;
using HockeyVenueManagement.Models.Rules;
using Microsoft.Extensions.Options;


namespace TennisBookings.Web.Domain.Rules
{
    public class MaxPeakTimeBookingLengthRule : IPitchBookingRule
    {
        private readonly IClubConfiguration _clubConfiguration;
        private readonly BookingConfiguration _bookingConfiguration;

        public MaxPeakTimeBookingLengthRule(IClubConfiguration clubConfiguration, IOptions<BookingConfiguration> options)
        {
            _clubConfiguration = clubConfiguration;
            _bookingConfiguration = options.Value;
        }

        public Task<bool> CompliesWithRuleAsync(PitchBooking booking)
        {
            if (booking.EndDateTime.Hour < _clubConfiguration.PeakStartHour)
                return Task.FromResult(true);

            var peakHours = 0;
            for (var hour = booking.StartDateTime.Hour; hour < booking.EndDateTime.Hour; hour++)
            {
                if (hour >= _clubConfiguration.PeakStartHour && hour <= _clubConfiguration.PeakEndHour)
                {
                    peakHours++;
                }
            }

            return Task.FromResult(peakHours <= _bookingConfiguration.MaxPeakBookingLengthInHours);
        }

        public string ErrorMessage => "The pitch booking is too long";
    }
}
