namespace HockeyVenueManagement.Models.Configuration
{
    public class BookingConfiguration : IBookingConfiguration
    {
        public int MaxRegularBookingLengthInHours { get; set; }

        public int MaxPeakBookingLengthInHours { get; set; }
    }
}