using HookVault.Domain.Entities;
using HookVault.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace HookVault.Application.DTOs
{

    public sealed record EndpointDto(Guid Id, Guid SourceId, string Slug, string DestinationUrls, bool IsActive,
        RetryPolicyDto RetryPolicy, DateTime CreatedAt, DateTime? UpdatedAt)
    {
        public static EndpointDto FromEntity(Endpoint endpoint) =>
            new(
                endpoint.Id,
                endpoint.SourceId,
                endpoint.Slug,
                endpoint.DestinationUrls,
                endpoint.IsActive,

                new RetryPolicyDto(
                    endpoint.Policy.MaxRetries,
                    endpoint.Policy.InitialDelaySeconds,
                    endpoint.Policy.BackoffMultiplier,
                    endpoint.Policy.MaxDelaySeconds),
                endpoint.CreatedAt,
                endpoint.UpdatedAt == endpoint.CreatedAt ? null : endpoint.UpdatedAt);

    }
}
