namespace LoadBalancer.Models;

public abstract class Server
{
    public string Url { get; set; }
    public int Count { get; set; }
    public int OriginalCount { get; set; }
}