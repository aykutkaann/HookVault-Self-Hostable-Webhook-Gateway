using HookVault.Application.Abstractions.Messaging;
using HookVault.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace HookVault.Application.WebhookEvents.Queries.GetWebhookEventById
{
    public sealed record GetWebhookEventByIdQuery(Guid Id) : IQuery<WebhookEventDetailDto?>;
    
    
}
