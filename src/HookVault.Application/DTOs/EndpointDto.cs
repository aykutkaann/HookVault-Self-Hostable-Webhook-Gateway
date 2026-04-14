using HookVault.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace HookVault.Application.DTOs
{

    public sealed record EndpointDto(Guid Id, Guid SourceId, string Slug, string DestinationUrls, bool IsActive,
        RetryPolicyDto RetryPolicy, DateTime CreatedAt, DateTime? UpdatedAt);
}
