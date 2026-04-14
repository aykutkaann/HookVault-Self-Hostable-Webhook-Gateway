using HookVault.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace HookVault.Domain.Entities
{
    public class Endpoint
    {
        public Guid Id { get; private set; }
        public Guid SourceId { get; private set; }
        public  Source Source { get; private set; } = null!;


        public string Slug { get; private set; } = null!;
        public string DestinationUrls { get; private set; } = null!;
        public bool IsActive { get; private set; }
        public RetryPolicy Policy { get; private set; } = null!;

        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }

        private static readonly Regex SlugRegex = new(@"^[a-z0-9][a-z0-9-]*$", RegexOptions.Compiled);
        private Endpoint() { }

        public Endpoint(Guid sourceId, string slug, string destinationUrls,  RetryPolicy policy)
        {

            ValidateSlug(slug);
            ValidateUrl(destinationUrls);

            Id = Guid.NewGuid();
            SourceId = sourceId;
            Slug = slug.ToLowerInvariant().Trim();
            DestinationUrls = destinationUrls;
            IsActive = true; // Varsayılan olarak aktif
            Policy = policy ?? RetryPolicy.Default;

            CreatedAt = DateTime.UtcNow;
            UpdatedAt = CreatedAt;

        }

        public void UpdateDestination(string newUrls)
        {
            ValidateUrl(newUrls);
            DestinationUrls = newUrls;

            Touch();
        }

        public void Activate()
        {
            IsActive = true;
            Touch();
        }

        public void Deactivate()
        {
            IsActive = false;
            Touch();
        }

        public void UpdateRetryPolicy(RetryPolicy newPolicy)
        {
            Policy = newPolicy ?? throw new ArgumentNullException(nameof(newPolicy));
            Touch();
        }

        //Private helper methods

        private void Touch()
        {
            UpdatedAt = DateTime.UtcNow;
        }

        private static void ValidateSlug(string slug)
        {
            if (string.IsNullOrWhiteSpace(slug))
                throw new ArgumentException("Slug cannot be empty.");

            if (!SlugRegex.IsMatch(slug))
                throw new ArgumentException("Slug must be lowercase, alphanumeric and may contain hyphens (cannot start with hyphen).");
        }

        private static void ValidateUrl(string url)
        {
            if (!Uri.TryCreate(url, UriKind.Absolute, out var uri))
                throw new ArgumentException("Invalid destination URL format.");

            var allowedSchemes = new[] { Uri.UriSchemeHttp, Uri.UriSchemeHttps };
            if (!allowedSchemes.Contains(uri.Scheme))
                throw new ArgumentException("Only HTTP and HTTPS schemes are allowed for security reasons.");
        }

    }
}
