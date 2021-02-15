using HockeyVenueManagement.Models;
using System;
using System.Threading.Tasks;


namespace HockeyVenueManagement.Services
{
    public interface IBookingService
    {
        Task<HourlyAvailabilityDictionary> GetBookingAvailabilityForDateAsync(DateTime date);

        Task<int> GetMaxBookingSlotForCourtAsync(DateTime startTime, DateTime endTime, int courtId);
    }
}