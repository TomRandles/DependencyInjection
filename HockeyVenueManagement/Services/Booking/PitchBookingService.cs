using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HockeyVenueManagement.DataAccess;
using HockeyVenueManagement.Models;
using HockeyVenueManagement.Services.Time;
using Microsoft.EntityFrameworkCore;

namespace HockeyVenueManagement.Services
{
    public class PitchBookingService : IPitchBookingService
    {
        private readonly HockeyBookingDbContext _dbContext;
        private readonly IUtcTimeService _utcTimeService;

        public PitchBookingService(HockeyBookingDbContext dbContext, IUtcTimeService utcTimeService)
        {
            _dbContext = dbContext;
            _utcTimeService = utcTimeService;
        }

        public async Task CreatePitchBooking(PitchBooking pitchBooking)
        {
            _dbContext.PitchBookings.Add(pitchBooking);

            await _dbContext.SaveChangesAsync();
        }

        public async Task<PitchBooking> LoadBooking(int bookingId)
        {
            var booking = await _dbContext.PitchBookings
                .AsNoTracking()
                .Include(x => x.Pitch)
                .Include(x => x.Member)
                .SingleOrDefaultAsync(x => x.Id == bookingId);

            return booking;
        }

        public async Task<bool> CancelBooking(int bookingId)
        {
            var booking = await _dbContext.PitchBookings.FindAsync(bookingId);

            if (booking == null)
                return false;

            _dbContext.PitchBookings.Remove(booking);

            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<PitchBooking>> BookingsUntilDateAsync(DateTime date)
        {
            var bookings = await _dbContext.PitchBookings
                .AsNoTracking()
                .Include(x => x.Pitch)
                .Include(x => x.Member)
                .Where(x => x.StartDateTime >= _utcTimeService.CurrentUtcDateTime && x.EndDateTime < date.Date.AddDays(1).AddMilliseconds(-1))
                .ToListAsync();

            return bookings;
        }

        public async Task<IEnumerable<PitchBooking>> BookingsForDayAsync(DateTime date)
        {
            var bookings = await _dbContext.PitchBookings
                .AsNoTracking()
                .Where(x => x.StartDateTime >= date.Date && x.EndDateTime < date.Date.AddDays(1).AddMilliseconds(-1))
                .ToListAsync();

            return bookings;
        }

        public async Task<IEnumerable<PitchBooking>> PitchBookingsForDayAsync(DateTime date, int pitchId)
        {
            var bookings = await _dbContext.PitchBookings
                .AsNoTracking()
                .Where(x => x.StartDateTime >= date.Date && x.EndDateTime < date.Date.AddDays(1).AddMilliseconds(-1) && x.PitchId == pitchId)
                .ToListAsync();

            return bookings;
        }

        public async Task<IEnumerable<PitchBooking>> MemberBookingsForDayAsync(DateTime date, Member member)
        {
            var bookings = await _dbContext.PitchBookings
                .AsNoTracking()
                .Where(x => x.StartDateTime >= date.Date && x.EndDateTime < date.Date.AddDays(1).AddMilliseconds(-1) && x.Member == member)
                .ToListAsync();

            return bookings;
        }

        public async Task<IEnumerable<PitchBooking>> GetFutureBookingsForMemberAsync(Member member)
        {
            return await _dbContext.PitchBookings
                .AsNoTracking()
                .Where(c => c.Member == member && c.StartDateTime >= DateTimeOffset.UtcNow)
                .OrderBy(x => x.StartDateTime)
                .ToListAsync();
        }

        public async Task<int> GetBookedHoursForMemberAsync(int memberId, DateTime date)
        {
            var member = await _dbContext.Members.FindAsync(memberId);

            if (member == null)
                throw new Exception("Member not found"); // should have better error handling here

            return await GetBookedHoursForMemberAsync(member, date);
        }

        public async Task<int> GetBookedHoursForMemberAsync(Member member, DateTime date)
        {
            var bookings = await _dbContext.PitchBookings
                .AsNoTracking()
                .Where(c => c.Member == member && c.StartDateTime >= date.Date && c.EndDateTime <= date.Date.AddDays(1).AddMilliseconds(-1))
                .ToListAsync();

            var hoursBooked = 0;

            foreach (var booking in bookings)
            {
                var length = (booking.EndDateTime - booking.StartDateTime).Hours;
                hoursBooked = hoursBooked + length;
            }

            return hoursBooked;
        }
    }
}
