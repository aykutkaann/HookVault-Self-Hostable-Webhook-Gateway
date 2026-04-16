using HookVault.Domain.Entities;
using HookVault.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HookVault.Infrastructure.Persistence.Repositories
{
    public sealed class WebhookEventRepository(HookVaultDbContext db) : IWebhookEventRepository
    {
        public async Task<WebHookEvent?> GetByIdAsync(Guid id, CancellationToken ct)
        {
            return await db.WebHookEvents.FirstOrDefaultAsync(w => w.Id == id, ct);
        }

        public async Task<WebHookEvent?> GetByIdempotencyKeyAsync(string key, CancellationToken ct)
        {
            return await db.WebHookEvents.FirstOrDefaultAsync(w => w.IdempotencyKey == key, ct);
        }

        public async Task AddAsync(WebHookEvent webhookEvent, CancellationToken ct)
        {
            await db.WebHookEvents.AddAsync(webhookEvent, ct);
        }

        public void Update(WebHookEvent webhookEvent)
        {
            db.WebHookEvents.Update(webhookEvent);
        }

    }
}
