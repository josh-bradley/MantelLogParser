using System.Collections.Generic;
using System.Linq;
using Mantel.LogParser;
using Xunit;

namespace Mantel.LogParserTests
{
    public class HttpLogTests
    {
        [Fact]
        public void ShouldCalculateUniqueEntriesCorrectly()
        {
            var logEntries = new List<LogEntry>
            {
                new("175.52.56.01", "/someurl"),
                new("185.14.56.01", "/someotherurl"),
                new("65.52.56.01", "/someurl"),
                new("22.52.56.01", "/someurl"),
            };

            var httpLog = new HttpLog(logEntries);
            var uniqueCount = httpLog.GetUniqueIpAddressCount();
            
            Assert.Equal(4, uniqueCount);
        }
        
        [Fact]
        public void ShouldHandleDuplicateIpAddresses()
        {
            var logEntries = new List<LogEntry>
            {
                new("175.52.56.01", "/someurl"),
                new("175.52.56.01", "/someotherurl"),
                new("65.52.56.01", "/someurl"),
                new("22.52.56.01", "/someurl"),
            };

            var httpLog = new HttpLog(logEntries);
            var uniqueCount = httpLog.GetUniqueIpAddressCount();
            
            Assert.Equal(3, uniqueCount);
        }
        
        [Fact]
        public void ShouldGetMostVisitedUrlsCorrectly()
        {
            var logEntries = new List<LogEntry>
            {
                new("175.52.56.01", "/someurl"),
                new("175.52.56.01", "/someotherurl"),
                new("65.52.56.01", "/someurl"),
                new("22.52.56.01", "/someurl"),
                new("175.52.56.01", "/someotherurl"),
                new("175.52.56.01", "/anotherurl"),
            };

            var httpLog = new HttpLog(logEntries);
            var mostVisitedUrl = httpLog.GetMostVisitedUrl().ToList();
            
            Assert.Equal("/someurl", mostVisitedUrl.First().Value);
            Assert.Equal("/someotherurl", mostVisitedUrl[1].Value);
            Assert.Equal("/anotherurl", mostVisitedUrl[2].Value);
        }
        
        [Fact]
        public void ShouldOrderByUrlsWhenThereAreTies()
        {
            var logEntries = new List<LogEntry>
            {
                new("175.52.56.01", "/zebras"),
                new("175.52.56.01", "/apples")
            };

            var httpLog = new HttpLog(logEntries);
            var mostVisitedUrl = httpLog.GetMostVisitedUrl().ToList();
            
            Assert.Equal("/apples", mostVisitedUrl.First().Value);
            Assert.Equal("/zebras", mostVisitedUrl[1].Value);
        }
        
        [Fact]
        public void ShouldGetMostActiveIpAddressesCorrectly()
        {
            var logEntries = new List<LogEntry>
            {
                new("175.52.56.01", "/someurl"),
                new("175.52.56.01", "/someotherurl"),
                new("175.52.56.01", "/someotherurl"),
                new("65.52.56.01", "/someurl"),
                new("65.52.56.01", "/someurl"),
                new("22.52.56.01", "/someurl"),
            };

            var httpLog = new HttpLog(logEntries);
            var orderedIpAddresses = httpLog.GetMostActiveIpAddress().ToList();
            
            Assert.Equal("175.52.56.01", orderedIpAddresses.First().Value);
            Assert.Equal("65.52.56.01", orderedIpAddresses[1].Value);
            Assert.Equal("22.52.56.01", orderedIpAddresses[2].Value);
        }
        
        [Fact]
        public void ShouldOrderByIpAddressWhenThereAreTies()
        {
            var logEntries = new List<LogEntry>
            {
                new("200.52.56.01", "/someotherurl"),
                new("175.52.56.01", "/someurl")
            };

            var httpLog = new HttpLog(logEntries);
            var orderedIpAddresses = httpLog.GetMostActiveIpAddress().ToList();
            
            Assert.Equal("175.52.56.01", orderedIpAddresses.First().Value);
            Assert.Equal("200.52.56.01", orderedIpAddresses[1].Value);
        }
    }
}