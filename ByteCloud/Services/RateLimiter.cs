using System.Net;

namespace RateLimiting.Services;

public class RateLimiter
{
    // Stores rate-limit data for each IP address.
    private readonly Dictionary<IPAddress, RateLimitEntry> _entries = new();

    // Rate limiter configuration.
    private readonly int _maxAttempts;
    private readonly TimeSpan _attemptWindow;
    private readonly TimeSpan _blockDuration;

    // Configure how many attempts are allowed,
    // how long attempts are counted, and how long to block.
    public RateLimiter(
        int maxAttempts,
        TimeSpan attemptWindow,
        TimeSpan blockDuration)
    {
        _maxAttempts = maxAttempts;
        _attemptWindow = attemptWindow;
        _blockDuration = blockDuration;
    }

    // Stores failed attempts and the time an IP is blocked until.
    private class RateLimitEntry
    {
        public List<DateTimeOffset> FailedAttempts { get; set; } = new();

        public DateTimeOffset BlockedUntil { get; set; }
    }

    // Checks whether an IP address is currently allowed.
    public bool IsAllowed(IPAddress ipAddress)
    {
        // No entry means the IP has no recorded failures.
        if (!_entries.ContainsKey(ipAddress))
        {
            return true;
        }

        RateLimitEntry entry = _entries[ipAddress];

        // If the block expiration is still in the future,
        // the IP is currently blocked.
        if (entry.BlockedUntil > DateTimeOffset.UtcNow)
        {
            return false;
        }

        return true;
    }

    // Records a failed attempt for an IP address.
    public void RecordFailedAttempt(IPAddress ipAddress)
    {
        // Create an entry if this IP has never failed before.
        if (!_entries.ContainsKey(ipAddress))
        {
            _entries[ipAddress] = new RateLimitEntry();
        }

        RateLimitEntry entry = _entries[ipAddress];

        DateTimeOffset now = DateTimeOffset.UtcNow;

        // Remove attempts that are outside the time window.
        entry.FailedAttempts.RemoveAll(
            attempt => now - attempt > _attemptWindow
        );

        // Record the new failed attempt.
        entry.FailedAttempts.Add(now);

        // Block the IP when it reaches the maximum attempts.
        if (entry.FailedAttempts.Count >= _maxAttempts)
        {
            entry.BlockedUntil = now.Add(_blockDuration);
        }
    }

    // Removes all rate-limit data for an IP address.
    public void Clear(IPAddress ipAddress)
    {
        _entries.Remove(ipAddress);
    }
}