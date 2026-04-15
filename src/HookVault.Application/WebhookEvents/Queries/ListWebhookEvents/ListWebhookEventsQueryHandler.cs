using HookVault.Application.Abstractions.Persistence;
using HookVault.Application.Common;
using HookVault.Application.DTOs;
using MediatR;


namespace HookVault.Application.WebhookEvents.Queries.ListWebhookEvents
{
    public sealed class ListWebhookEventsQueryHandler(IWebhookEventReadService readService)
        : IRequestHandler<ListWebhookEventsQuery, PagedResult<WebhookEventListItemDto>>
    {
        public async Task<PagedResult<WebhookEventListItemDto>> Handle(ListWebhookEventsQuery request, CancellationToken cancellationToken)
        {



            return await readService.QueryAsync(request, cancellationToken);
        }
    }
}
