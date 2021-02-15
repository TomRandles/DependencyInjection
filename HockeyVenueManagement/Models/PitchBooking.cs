using System;

namespace HockeyVenueManagement.Models
{
    public class PitchBooking
    {
        public int Id { get; set; }

        public int PitchId { get; set; }

        public int MemberId { get; set; }

        public Member Member { get; set; }

        public Pitch Pitch { get; set; }

        public DateTime StartDateTime { get; set; }

        public DateTime EndDateTime { get; set; }
    }
}