using System.Collections.Generic;
using System.Linq;

namespace Mantel.LogParser
{
    public class ItemCount
    {
        public string Value { get; set; }
        public int Count { get; set; }
    }

    public class HttpLog
    {
        private readonly IEnumerable<LogEntry> logEntries;

        public HttpLog(IEnumerable<LogEntry> logEntries)
        {
            this.logEntries = logEntries;
        }
        
        public int GetUniqueIpAddressCount()
        {
            return logEntries
                        .Select(x => x.IpAddress)
                        .Distinct()
                        .Count();
        }

        public IEnumerable<ItemCount> GetMostVisitedUrl()
        {
            return logEntries
                    .GroupBy(x => x.Url)
                    .OrderByDescending(x => x.Count())
                    .ThenBy(x => x.Key)
                    .Select(g => new ItemCount {Value = g.Key, Count = g.Count()});
        }

        public IEnumerable<ItemCount> GetMostActiveIpAddress()
        {
            return logEntries
                    .GroupBy(x => x.IpAddress)
                    .OrderByDescending(x => x.Count())
                    .ThenBy(x => x.Key)
                    .Select(g => new ItemCount {Value = g.Key, Count = g.Count()});
        }
    }
}