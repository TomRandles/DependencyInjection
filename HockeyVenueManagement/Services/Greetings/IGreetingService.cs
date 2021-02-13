namespace HockeyVenueManagement.Services.Greetings
{
    public interface IGreetingService
    {
        string GetRandomGreeting();

        string GetRandomLoginGreeting(string name);
    }
}