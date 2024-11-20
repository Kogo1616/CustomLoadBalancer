using LoadBalancer.Models;
using Microsoft.Extensions.Configuration;

namespace LoadBalancer.Service;

public class LoadBalancer
{
    private readonly List<Server> _servers;
    private readonly HttpClient _httpClient;
    private readonly Random _random;

    public LoadBalancer(IConfiguration configuration)
    {
        _httpClient = new HttpClient();
        _random = new Random();
        _servers = configuration.GetSection("LoadBalancerSettings:Servers")
                                 .Get<List<Server>>();
    }

    public async Task<string> GetUrlAsync()
    {
        // Check if all servers have reached their count limit
        bool allServersAtLimit = true;
        foreach (var server in _servers)
        {
            if (server.Count > 0)
            {
                allServersAtLimit = false;
                break;
            }
        }

        // If all servers have reached the limit, reset counts
        if (allServersAtLimit)
        {
            Console.WriteLine("All servers have reached their limit, resetting counts...");
            foreach (var server in _servers)
            {
                server.Count = server.OriginalCount; // Reset count
            }
        }

        // Select a server that is not at its count limit
        var availableServers = _servers.FindAll(s => s.Count > 0);
        if (availableServers.Count == 0) return null;

        var selectedServer = availableServers[_random.Next(availableServers.Count)];

        selectedServer.Count--;

        if (_random.NextDouble() < 0.3)
        {
            Console.WriteLine($"Simulated failure on {selectedServer.Url}, trying another...");
            return await GetUrlAsync();
        }

        Console.WriteLine($"Requesting {selectedServer.Url}...");
        return selectedServer.Url;
    }

    public async Task<string> SendRequestAsync()
    {
        var url = await GetUrlAsync();
        if (url == null) return "All servers have reached their request limit and were reset.";

        try
        {
            var response = await _httpClient.GetStringAsync(url);
            return response;
        }
        catch (Exception ex)
        {
            return $"Error calling server: {ex.Message}";
        }
    }
}