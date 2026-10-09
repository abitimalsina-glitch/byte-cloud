namespace Normalizer.Services;

public static class AccountNormalizer
{
    public static string NormalizeEmail(string email)
    {
        return email.Trim().ToLowerInvariant();
    }
}