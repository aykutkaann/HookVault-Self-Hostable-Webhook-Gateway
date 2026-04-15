using HookVault.Application.Abstractions.Messaging;
using HookVault.Application.Common;
using HookVault.Application.DTOs;
using HookVault.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace HookVault.Application.WebhookEvents.Queries.ListWebhookEvents
{
    public sealed record ListWebhookEventsQuery(Guid? SourceId, Guid? EndpointId, WebhookStatus? Status, DateTime? From, DateTime? To,
        int Page = 1, int PageSize = 50) : IQuery<PagedResult<WebhookEventListItemDto>>;
    
    
}
