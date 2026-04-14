using HookVault.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HookVault.Domain.Repositories
{
    public interface IWebhookEventRepository
    {
        Task<WebHookEvent?> GetByIdAsync(Guid id, CancellationToken ct);
        Task<WebHookEvent?> GetByIdempotencyKeyAsync(string key, CancellationToken ct);
        Task AddAsync(WebHookEvent webhookEvent, CancellationToken ct);
        void Update(WebHookEvent webhookEvent);
    }
}
