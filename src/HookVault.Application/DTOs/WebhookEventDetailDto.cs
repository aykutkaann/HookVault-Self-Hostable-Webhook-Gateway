using HookVault.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace HookVault.Infrastructure.DTOs
{

    public sealed record WebhookEventDetailDto(
        Guid Id,
        Guid EndpointId,
        string SourceName,
        string HttpMethod,
        IReadOnlyDictionary<string, string> Headers,
        string RawBody,
        string ContentType,
        SignatureStatus SignatureStatus,
        string? IdempotencyKey,
        WebhookStatus Status,
        DateTime ReceivedAt,
        DateTime? DeliveredAt,
        IReadOnlyList<DeliveryAttemptDto> DeliveryAttempts);
}
