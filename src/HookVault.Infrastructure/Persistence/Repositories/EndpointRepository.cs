using HookVault.Domain.Entities;
using HookVault.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HookVault.Infrastructure.Persistence.Repositories
{
    public sealed  class EndpointRepository(HookVaultDbContext db): IEndpointRepository
    {
        public async Task<Endpoint?> GetByIdAsync(Guid id, CancellationToken ct)
        {
            return await db.Endpoints.FirstOrDefaultAsync(e => e.Id == id, ct);
        }

        public async Task<Endpoint?> GetBySourceAndSlugAsync(string sourceName, string slug, CancellationToken ct)
        {
            return await db.Endpoints.Include(e => e.Source).FirstOrDefaultAsync(e => e.Source.Name == sourceName && e.Slug == slug, ct);
        }

        public async Task<IReadOnlyList<Endpoint>> ListBySourceAsync(Guid sourceId, CancellationToken ct)
        {
            return await db.Endpoints.Where(e => e.SourceId == sourceId).ToListAsync(ct);
        }

        public async Task AddAsync(Endpoint endpoint, CancellationToken ct)
        {
            await db.Endpoints.AddAsync(endpoint, ct);
        }

        public void Update(Endpoint endpoint)
        {
            db.Endpoints.Update(endpoint);
        }

        public void Remove(Endpoint endpoint)
        {
            db.Endpoints.Remove(endpoint);
        }
    }
}
