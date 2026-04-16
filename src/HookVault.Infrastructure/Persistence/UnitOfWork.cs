using HookVault.Application.Abstractions.Persistence;


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
