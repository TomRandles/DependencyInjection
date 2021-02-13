using System;

namespace HockeyVenueManagement.Services.Audit
{
    public class ConsoleAuditor : IAuditor
    {
        public string SourceName { get; }

        public ConsoleAuditor(string sourceName)
        {
            SourceName = sourceName;
        }

        public void RecordAction(string message)
        {
            Console.WriteLine($"{SourceName}: {message}");
        }
    }
}
