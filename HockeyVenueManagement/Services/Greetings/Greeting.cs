using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading;

namespace HockeyVenueManagement.Services.Greetings
{

    public class GreetingService : IGreetingService
    {
        private static readonly ThreadLocal<Random> Random
            = new ThreadLocal<Random>(() => new Random());

        private readonly Guid _instanceId = Guid.NewGuid();

        public GreetingService(IWebHostEnvironment hostingEnvironment)
        {
            var webRootPath = hostingEnvironment.WebRootPath;

            var greetingsJson = System.IO.File.ReadAllText(webRootPath + "/greetings.json");

            var greetingsData = JsonConvert.DeserializeObject<GreetingData>(greetingsJson);

            Greetings = greetingsData.Greetings;

            LoginGreetings = greetingsData.LoginGreetings;
        }

        public string[] Greetings { get; }

        public string[] LoginGreetings { get; }

        public string GetRandomGreeting()
        {
            return GetRandomValue(Greetings) + " " + _instanceId;
        }

        public string GetRandomLoginGreeting(string name)
        {
            var loginGreeting = GetRandomValue(LoginGreetings);

            return loginGreeting.Replace("{name}", name);
        }

        private string GetRandomValue(IReadOnlyList<string> greetings)
        {
            if (greetings.Count == 0) return string.Empty;

            var greetingToUse = Random.Value.Next(greetings.Count);

            return greetingToUse >= 0 ? greetings[greetingToUse] : string.Empty;
        }

        private class GreetingData
        {
            public string[] Greetings { get; set; }

            public string[] LoginGreetings { get; set; }
        }
    }
}
