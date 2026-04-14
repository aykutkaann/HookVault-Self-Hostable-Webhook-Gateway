using System;
using System.Collections.Generic;
using System.Text;

namespace HookVault.Infrastructure.Abstractions
{
    public interface IIdempotencyStore
    {
        Task<bool> TryReserveAsync(string idempotencyKey, TimeSpan ttl, CancellationToken ct);
        Task<Guid?> GetExistingEventIdAsync(string idempotencyKey, CancellationToken ct);
    }
}
