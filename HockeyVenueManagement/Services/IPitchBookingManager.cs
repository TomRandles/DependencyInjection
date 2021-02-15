using HockeyVenueManagement.Models;
using System;
using System.Threading.Tasks;

namespace TennisBookings.Web.Services
{
    public interface IPitchBookingManager
    {
        Task<PitchBookingResult> MakeBookingAsync(DateTime startDateTime, DateTime endDateTime, int courtId, Member member);
    }
}
