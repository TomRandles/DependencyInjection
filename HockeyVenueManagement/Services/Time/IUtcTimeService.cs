using System;

namespace HockeyVenueManagement.Services.Time
{
    public interface IUtcTimeService
    {
        DateTime CurrentUtcDateTime { get; }
    }
}