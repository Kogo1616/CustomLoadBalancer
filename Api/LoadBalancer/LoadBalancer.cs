using System.Collections.Immutable;

namespace LoadBalancer
{
    public class LoadBalancerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly List<string> _responses;

        public async Task Invoke(HttpContext context)
        {
            // Get the next response in a thread-safe way
            string response = "Hello";

            // Return the response
            context.Response.ContentType = "text/plain";
            await context.Response.WriteAsync(response);
        }
    }
}