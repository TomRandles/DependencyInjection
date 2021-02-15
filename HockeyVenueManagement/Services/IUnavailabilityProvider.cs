using HockeyVenueManagement.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HockeyVenueManagement.Services
{
    public interface IUnavailabilityProvider
    {
        Task<IEnumerable<HourlyUnavailability>> GetHourlyUnavailabilityAsync(DateTime date);

        Task<IEnumerable<int>> GetHourlyUnavailabilityAsync(DateTime date, int courtId);
    }

}
