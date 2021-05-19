namespace Mantel.LogParser
{
    public class LogEntry
    {
        public LogEntry(string ipAddress, string url)
        {
            IpAddress = ipAddress;
            Url = url;
        }
        
        public string Url { get; }
        public string IpAddress { get; }
    }
}