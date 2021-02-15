using System;
using System.Collections.Generic;

namespace HockeyVenueManagement.Models
{
    public class Member
    {
        public int Id { get; set; }

        public string Forename { get; set; }

        public string Surname { get; set; }
        
        public DateTime JoinDate { get; set; }

        public ICollection<PitchBooking> PitchBookings { get; set; }

        public string UserId { get; set; }

        public HockeyBookingsUser User { get; set; }
    }
}
