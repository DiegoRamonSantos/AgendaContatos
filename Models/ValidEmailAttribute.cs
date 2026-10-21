using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace AgendaContatos.Models;

public sealed class ValidEmailAttribute : ValidationAttribute
{
    private static readonly Regex EmailRegex = new(
        @"^[a-z0-9_%+\-]+(?:\.[a-z0-9_%+\-]+)*@(?:[a-z0-9](?:[a-z0-9\-]{0,61}[a-z0-9])?\.)+(?<tld>[a-z]{2,24})$",
        RegexOptions.IgnoreCase | RegexOptions.CultureInvariant,
        TimeSpan.FromMilliseconds(250));

    private static readonly HashSet<string> AllowedTopLevelDomains = new(StringComparer.OrdinalIgnoreCase)
    {
        "com",
        "org",
        "net",
        "br",
        "edu",
        "gov",
        "mil",
        "int",
        "io",
        "co",
        "info",
        "biz",
        "me",
        "app",
        "dev",
        "ai",
        "tech",
        "online",
        "site",
        "store",
        "cloud",
        "digital",
        "software",
        "systems",
        "agency",
        "company",
        "solutions",
        "services",
        "pro",
        "name",
        "tv",
        "cc",
        "us",
        "uk",
        "ca",
        "de",
        "fr",
        "es",
        "it",
        "pt",
        "ar",
        "cl",
        "mx",
        "au",
        "jp",
        "cn",
        "in"
    };

    public override bool IsValid(object? value)
    {
        if (value is null)
        {
            return true;
        }

        var email = value.ToString()?.Trim();

        if (string.IsNullOrWhiteSpace(email))
        {
            return true;
        }

        if (email.Length > 160)
        {
            return false;
        }

        var atSignIndex = email.IndexOf('@', StringComparison.Ordinal);

        if (atSignIndex <= 0 || atSignIndex > 64)
        {
            return false;
        }

        var match = EmailRegex.Match(email);

        return match.Success && AllowedTopLevelDomains.Contains(match.Groups["tld"].Value);
    }
}
