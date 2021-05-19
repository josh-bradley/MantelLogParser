using System;
using System.IO;
using System.Linq;

namespace Mantel.LogParser
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Please pass a file path.");
                return;
            }

            var logfilePath = args[0];
            if (!File.Exists(logfilePath))
            {
                Console.WriteLine("Could not find file.");
                return;
            }
            var lines = File.ReadAllLines(logfilePath);

            var logParser = new HttpLogParser();
            var logEntries = lines
                                                .Where(line => !string.IsNullOrWhiteSpace(line))
                                                .Select(line => logParser.ParseLogEntry(line));

            var httpLog = new HttpLog(logEntries);
            
            Console.WriteLine($"Unique IpAdresses {httpLog.GetUniqueIpAddressCount()}");
            Console.WriteLine("");
            var orderedIpAddress = httpLog.GetMostActiveIpAddress();
            Console.WriteLine("Top 3 Most Active Ip Addresses");

            foreach (var current in orderedIpAddress.Take(3))
            {
                Console.WriteLine($"{current.Value} Entries: {current.Count}");
            }

            Console.WriteLine("");
            var mostVisitedOrdered = httpLog.GetMostVisitedUrl();
            Console.WriteLine("Top 3 Most Visited Urls");
            
            foreach (var current in mostVisitedOrdered.Take(3))
            {
                Console.WriteLine($"{current.Value} Entries: {current.Count}");
            }
        }
    }
}
