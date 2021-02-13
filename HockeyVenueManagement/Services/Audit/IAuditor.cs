namespace HockeyVenueManagement.Services.Audit
{
    public interface IAuditor<out T> : IAuditor
    {
    }

    public interface IAuditor
    {
        void RecordAction(string message);
    }
}