using HookVault.Application.Abstractions.Persistence;
using HookVault.Domain.Entities;
using HookVault.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HookVault.Infrastructure.Persistence.Repositories
{
    public sealed class SourceRepository(HookVaultDbContext db) :ISourceRepository
    {
        public async Task<Source?> GetByIdAsync(Guid id, CancellationToken ct)
        {
            return await db.Sources.FirstOrDefaultAsync(s => s.Id == id, ct);
        }

        public async Task<Source?> GetByNameAsync(string name, CancellationToken ct)
        {
            return await db.Sources.FirstOrDefaultAsync(s => s.Name == name, ct);
        }

        public async Task<IReadOnlyList<Source>> ListAsync(CancellationToken ct)
        {
            return await db.Sources.AsNoTracking().ToListAsync(ct);
        }

        public async Task AddAsync(Source source, CancellationToken ct)
        {
            await db.Sources.AddAsync(source, ct);

        }

        public  void Update(Source source)
        {
            db.Sources.Update(source);

        }

        public void Remove(Source source)
        {
            db.Sources.Remove(source);
        }
    }
}
