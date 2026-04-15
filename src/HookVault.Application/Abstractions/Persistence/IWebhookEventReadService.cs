using HookVault.Application.Common;
using HookVault.Application.DTOs;
using HookVault.Application.WebhookEvents.Queries;
using HookVault.Application.WebhookEvents.Queries.ListWebhookEvents;
using System;
using System.Collections.Generic;
using System.Text;

namespace HookVault.Application.Abstractions.Persistence
{
    public interface IWebhookEventReadService
    {
        Task<PagedResult<WebhookEventListItemDto>> QueryAsync(ListWebhookEventsQuery query, CancellationToken ct);
        Task<WebhookEventDetailDto?> GetDetailAsync(Guid id, CancellationToken ct);
    }
}
