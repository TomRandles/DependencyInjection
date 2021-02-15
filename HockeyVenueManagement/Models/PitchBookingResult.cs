using System.Collections.Generic;

namespace HockeyVenueManagement.Models
{
    public class PitchBookingResult
    {
        private PitchBookingResult(PitchBooking booking, bool passedRules, IEnumerable<string> errors)
        {
            PitchBooking = booking;
            BookingSuccessful = passedRules;
            Errors = errors;
        }

        public PitchBooking PitchBooking { get; }

        public bool BookingSuccessful { get; }

        public IEnumerable<string> Errors { get; }

        public static PitchBookingResult Success(PitchBooking PitchBooking) => new PitchBookingResult(PitchBooking, true, null);

        public static PitchBookingResult Failure(IEnumerable<string> errors) => new PitchBookingResult(null, false, errors);
    }
}
