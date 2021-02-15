using System;
using System.Threading.Tasks;
using HockeyVenueManagement.Models;

namespace HockeyVenueManagement.Services
{
    public interface IPitchBookingManager
    {
        Task<PitchBookingResult> MakeBookingAsync(DateTime startDateTime, 
                                                  DateTime endDateTime, 
                                                  int pitchId, 
                                                  Member member);
    }
}
