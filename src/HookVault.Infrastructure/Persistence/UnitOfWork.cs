using HookVault.Application.Abstractions.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace HookVault.Infrastructure.Persistence
{
    public sealed class UnitOfWork(HookVaultDbContext db) : IUnitOfWork
    {

        public async Task<int> SaveChangesAsync(CancellationToken ct)
        {

           return await db.SaveChangesAsync(ct);

        }
    }
}
