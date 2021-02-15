using HockeyVenueManagement.Models;
using HockeyVenueManagement.Models.Rules;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TennisBookings.Web.Domain.Rules
{
    public class BookingRuleProcessor : IBookingRuleProcessor
    {
        private readonly IEnumerable<IPitchBookingRule> _rules;

        public BookingRuleProcessor(IEnumerable<IPitchBookingRule> rules)
        {
            _rules = rules;
        }

        public async Task<(bool, IEnumerable<string>)> PassesAllRulesAsync(PitchBooking pitchBooking)
        {
            var passedRules = true;

            var errors = new List<string>();

            foreach (var rule in _rules)
            {
                if (!await rule.CompliesWithRuleAsync(pitchBooking))
                {
                    errors.Add(rule.ErrorMessage);
                    passedRules = false;
                }
            }

            return (passedRules, errors);
        }
    }
}
