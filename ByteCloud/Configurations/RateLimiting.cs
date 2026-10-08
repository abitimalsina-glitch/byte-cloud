using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.RateLimiting;

namespace RateLimiting.Configuration;

public static class RateLimitingConfig
{
    // Extension method used to register ByteCloud's rate-limiting
    // configuration with ASP.NET Core's dependency injection system.
    public static IServiceCollection AddByteCloudRateLimiting(
        this IServiceCollection services)
    {
        // Registers ASP.NET Core's built-in rate-limiting service.
        services.AddRateLimiter(options =>
        {
            // Creates a rate-limiting policy named "login".
            // This policy can later be applied to specific endpoints.
            options.AddPolicy("login", httpContext =>
            {
                // Get the IP address of the client making the request.
                // Each IP address will have its own rate-limit bucket.
                var ipAddress =
                    httpContext.Connection.RemoteIpAddress?.ToString()
                    ?? "unknown";

                // Create a fixed-window rate limiter for this IP address.
                return RateLimitPartition.GetFixedWindowLimiter(
                    // Use the client's IP address as the partition key.
                    // This gives each IP address its own independent limit.
                    partitionKey: ipAddress,

                    // Configure the rate limiter for this partition.
                    factory: _ => new FixedWindowRateLimiterOptions
                    {
                        // Allow a maximum of 5 requests in the window.
                        PermitLimit = 5,

                        // Reset the request limit every 1 minute.
                        Window = TimeSpan.FromMinutes(2),

                        // Do not place additional requests in a queue.
                        // Requests above the limit are rejected immediately.
                        QueueLimit = 0
                    });
            });
        });

        // Return the service collection so additional
        // service registrations can continue to be chained.
        return services;
    }
}