using System.Collections.Generic;
using System.Threading.Tasks;

namespace HockeyVenueManagement.Models.Rules
{
    public interface IBookingRuleProcessor
    {
        Task<(bool, IEnumerable<string>)> PassesAllRulesAsync(PitchBooking PitchBooking);
    }
}