using HookVault.Application.Abstractions.Persistence;
using HookVault.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HookVault.Application.WebhookEvents.Queries.GetWebhookEventById
{
    public sealed class GetWebhookEventByIdQueryHandler :IRequestHandler<GetWebhookEventByIdQuery, WebhookEventDetailDto?> 
    {
        private readonly IWebhookEventReadService _service;

        public GetWebhookEventByIdQueryHandler(IWebhookEventReadService service)
        {
            _service = service;
        }

        public async Task<WebhookEventDetailDto?> Handle(GetWebhookEventByIdQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetDetailAsync(request.Id, cancellationToken);
        }

    }
}
