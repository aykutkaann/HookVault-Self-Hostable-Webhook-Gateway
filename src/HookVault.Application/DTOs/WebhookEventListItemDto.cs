using HookVault.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace HookVault.Application.DTOs
{

    public sealed record WebhookEventListItemDto(
        Guid Id,
        Guid EndpointId,
        string SourceName,
        SignatureStatus SignatureStatus,
        WebhookStatus Status,
        DateTime ReceivedAt,
        DateTime? DeliveredAt,
        int? LastResponseStatusCode);
}
