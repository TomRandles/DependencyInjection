using System;
using System.Threading.Tasks;
using HockeyVenueManagement.Models;
using HockeyVenueManagement.Models.Rules;
using HockeyVenueManagement.Services.Messaging;

namespace HockeyVenueManagement.Services
{
    public class PitchBookingManager : IPitchBookingManager
    {
        private readonly IPitchBookingService _bookingService;
        private readonly IBookingRuleProcessor _bookingRuleProcessor;
        private readonly IMessagingService _messagingService;

        public PitchBookingManager(
            IPitchBookingService bookingService, 
            IBookingRuleProcessor bookingRuleProcessor, 
            IMessagingService messagingService)
        {
            _bookingService = bookingService;
            _bookingRuleProcessor = bookingRuleProcessor;
            _messagingService = messagingService;
        }

        public async Task<PitchBookingResult> MakeBookingAsync(DateTime startDateTime, 
                                                               DateTime endDateTime, 
                                                               int pitchId, 
                                                               Member member)
        {
            var pitchBooking = new PitchBooking
            {
                PitchId = pitchId,
                Member = member,
                StartDateTime = startDateTime,
                EndDateTime = endDateTime
            };

            var (passedRules, errors) = await _bookingRuleProcessor.PassesAllRulesAsync(pitchBooking);

            if (!passedRules)
                return PitchBookingResult.Failure(errors);

            await _bookingService.CreatePitchBooking(pitchBooking);

            await _messagingService.SendAsync("Thank you. Your booking is confirmed", member.User.Id);

            return PitchBookingResult.Success(pitchBooking);
        }
    }
}
