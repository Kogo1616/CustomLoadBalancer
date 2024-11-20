using Microsoft.Extensions.Configuration;

class Program
{
    public static async Task Main(string[] args)
    {
        // Build the configuration from appsettings.json
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var loadBalancer = new LoadBalancer.Service.LoadBalancer(configuration);

        Console.WriteLine("Press any key to make a request. Press 'Q' to quit.");

        while (true)
        {
            var key = Console.ReadKey(intercept: true).Key;

            if (key == ConsoleKey.Q)
            {
                break;
            }

            var result = await loadBalancer.SendRequestAsync();
            Console.WriteLine($"Result: {result}");
        }
    }
}