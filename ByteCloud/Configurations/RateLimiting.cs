using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.RateLimiting;

namespace RateLimiting.Configuration;

public static class RateLimitingConfig
{
    // Registers ByteCloud's rate-limiting configuration.
    public static IServiceCollection AddByteCloudRateLimiting(
        this IServiceCollection services)
    {
        // Register ASP.NET Core's built-in rate-limiting service.
        services.AddRateLimiter(options =>
        {
            // Rate-limit the login endpoint.
            options.AddPolicy("login", httpContext =>
            {
                // Get the client's IP address.
                var ipAddress =
                    httpContext.Connection.RemoteIpAddress?.ToString()
                    ?? "unknown";

                // Create an independent rate limiter for each IP.
                return RateLimitPartition.GetFixedWindowLimiter(
                    partitionKey: ipAddress,
                    factory: _ => new FixedWindowRateLimiterOptions
                    {
                        // Allow 5 requests per 2-minute window.
                        PermitLimit = 5,
                        Window = TimeSpan.FromMinutes(2),

                        // Reject excess requests immediately.
                        QueueLimit = 0
                    });
            });

            // Rate-limit the registration endpoint.
            options.AddPolicy("register", httpContext =>
            {
                var ipAddress =
                    httpContext.Connection.RemoteIpAddress?.ToString()
                    ?? "unknown";

                return RateLimitPartition.GetFixedWindowLimiter(
                    partitionKey: ipAddress,
                    factory: _ => new FixedWindowRateLimiterOptions
                    {
                        // Allow 5 registration requests per 2 minutes.
                        PermitLimit = 5,
                        Window = TimeSpan.FromMinutes(3),
                        QueueLimit = 0
                    });
            });
        });

        // Return services to support method chaining.
        return services;
    }
}