using System;

namespace HockeyVenueManagement.Models
{
    public class PitchMaintenanceSchedule
    {
        public int Id { get; set; }

        public string WorkTitle { get; set; }
        
        public bool PitchIsClosed { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int PitchId { get; set; }

        public Pitch Pitch { get; set; }
    }
}
