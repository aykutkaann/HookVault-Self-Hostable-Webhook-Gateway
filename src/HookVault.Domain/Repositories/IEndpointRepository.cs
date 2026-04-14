using HookVault.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HookVault.Domain.Repositories
{
    public interface IEndpointRepository
    {
        Task<Endpoint?> GetByIdAsync(Guid id, CancellationToken ct);
        Task<Endpoint?> GetBySourceAndSlugAsync(string sourceName, string slug, CancellationToken ct);
        Task<IReadOnlyList<Endpoint>> ListBySourceAsync(Guid sourceId, CancellationToken ct);
        Task AddAsync(Endpoint endpoint, CancellationToken ct);
        void Update(Endpoint endpoint);
        void Remove(Endpoint endpoint);
    }
}
