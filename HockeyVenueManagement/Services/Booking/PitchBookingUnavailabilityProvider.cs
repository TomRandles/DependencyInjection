using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HockeyVenueManagement.Models;

namespace HockeyVenueManagement.Services
{
    public class PitchBookingUnavailabilityProvider : IUnavailabilityProvider
    {
        private readonly IPitchBookingService _PitchBookingService;

        public PitchBookingUnavailabilityProvider(IPitchBookingService PitchBookingService)
        {
            _PitchBookingService = PitchBookingService;
        }

        public async Task<IEnumerable<HourlyUnavailability>> GetHourlyUnavailabilityAsync(DateTime date)
        {
            var bookings = await _PitchBookingService.BookingsForDayAsync(date);

            return BookingsToUnavailability(bookings);
        }

        public async Task<IEnumerable<int>> GetHourlyUnavailabilityAsync(DateTime date, int courtId)
        {
            var bookings = await _PitchBookingService.PitchBookingsForDayAsync(date, courtId);

            var availability = BookingsToUnavailability(bookings);

            return availability.Select(x => x.Hour);
        }

        private IEnumerable<HourlyUnavailability> BookingsToUnavailability(IEnumerable<PitchBooking> PitchBookings)
        {
            var unavailability = new List<HourlyUnavailability>();

            for (var i = 0; i < 24; i++)
            {
                unavailability.Add(new HourlyUnavailability(i, new HashSet<int>()));
            }

            foreach (var booking in PitchBookings)
            {
                for (var i = booking.StartDateTime.Hour; i < booking.EndDateTime.Hour; i++)
                {
                    unavailability.First(x => x.Hour == i).UnavailableCourtIds.Add(booking.PitchId);
                }
            }

            return unavailability.Where(x => x.UnavailableCourtIds.Any());
        }
    }
}