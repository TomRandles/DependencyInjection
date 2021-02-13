using System;

namespace HockeyVenueManagement.Services.Time
{
    public interface ITimeService
    {
        DateTime CurrentTime { get; }
    }
}