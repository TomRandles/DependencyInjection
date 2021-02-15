using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace HockeyVenueManagement.Models
{
    public class HockeyBookingsUser : IdentityUser
    {
        public virtual ICollection<PitchBooking> PitchBookings { get; set; }

        public virtual Member Member { get; set; }

        public DateTime LastRequestUtc { get; set; }
    }
}
