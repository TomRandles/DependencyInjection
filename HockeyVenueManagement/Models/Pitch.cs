using System.Collections.Generic;

namespace HockeyVenueManagement.Models
{
    public class Pitch
    {
        public int Id { get; set; }

        public PitchType Type { get; set; }

        public string Name { get; set; }

        public ICollection<PitchBooking> Bookings { get; set; }
    }
}
