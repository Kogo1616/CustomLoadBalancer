namespace LoadBalancer.Configs;

public class ServerConfig
{
    public string Url { get; set; }
    public int Count { get; set; } // Limit of requests
}