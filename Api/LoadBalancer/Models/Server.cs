namespace LoadBalancer.Models;

public class Server
{
    public string Url { get; set; }
    public bool IsHealty { get; set; }

    public Server(string url, bool isHealty)
    {
        Url = url;
        IsHealty = isHealty;
    }
}