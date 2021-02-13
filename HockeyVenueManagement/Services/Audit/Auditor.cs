using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HockeyVenueManagement.Services.Audit
{
    public class Auditor<T> : IAuditor<T>
    {
        private readonly IAuditor _auditor;

        public Auditor()
        {
            _auditor = new ConsoleAuditor(typeof(T).Name);
        }

        public void RecordAction(string message)
        {
            _auditor.RecordAction(message);
        }
    }
}
