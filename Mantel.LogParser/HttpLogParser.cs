using System;

namespace Mantel.LogParser
{
    public class HttpLogParser
    {
        public LogEntry ParseLogEntry(string logEntry)
        {
            if (string.IsNullOrWhiteSpace(logEntry))
            {
                throw new ArgumentException();
            }
            
            var items = logEntry.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            var ipAddress = items[0];
            var url = items[6];
            
            return new LogEntry(ipAddress, url);
        }
    }
}
