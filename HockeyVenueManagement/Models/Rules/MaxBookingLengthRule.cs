using HockeyVenueManagement.Models.Configuration;
using System;
using System.Threading.Tasks;

namespace HockeyVenueManagement.Models.Rules
{
    /// <summary>
    /// A rule which prevents a single booking being longer than the configured max booking.
    /// </summary>
    public class MaxBookingLengthRule : IPitchBookingRule
    {
        private readonly IBookingConfiguration _bookingConfiguration;

        public MaxBookingLengthRule(IBookingConfiguration bookingConfiguration)
        {
            _bookingConfiguration = bookingConfiguration;
        }

        public Task<bool> CompliesWithRuleAsync(PitchBooking booking)
        {
            var bookingLength = booking.EndDateTime - booking.StartDateTime;

            var compliesWithRule = bookingLength <= TimeSpan.FromHours(_bookingConfiguration.MaxRegularBookingLengthInHours);

            return Task.FromResult(compliesWithRule);
        }

        public string ErrorMessage => "Booking is longer than allowed booking length";
    }
}
