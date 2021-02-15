using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HockeyVenueManagement.Data;
using HockeyVenueManagement.Models;

namespace HockeyVenueManagement.Services
{
    public interface IPitchBookingService
    {
        Task CreatePitchBooking(PitchBooking pitchBooking);

        Task<bool> CancelBooking(int bookingId);

        Task<PitchBooking> LoadBooking(int bookingId);

        Task<IEnumerable<PitchBooking>> BookingsUntilDateAsync(DateTime date);

        Task<IEnumerable<PitchBooking>> BookingsForDayAsync(DateTime date);

        Task<IEnumerable<PitchBooking>> PitchBookingsForDayAsync(DateTime date, int pitchId);

        Task<IEnumerable<PitchBooking>> MemberBookingsForDayAsync(DateTime date, Member member);

        Task<int> GetBookedHoursForMemberAsync(int memberId, DateTime date);

        Task<IEnumerable<PitchBooking>> GetFutureBookingsForMemberAsync(Member member);

        Task<int> GetBookedHoursForMemberAsync(Member member, DateTime date);
    }
}
