using HookVault.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HookVault.Domain.Repositories
{

    public interface ISourceRepository
    {
        Task<Source?> GetByIdAsync(Guid id, CancellationToken ct);
        Task<Source?> GetByNameAsync(string name, CancellationToken ct);
        Task<IReadOnlyList<Source>> ListAsync(CancellationToken ct);
        Task AddAsync(Source source, CancellationToken ct);
        void Update(Source source);
        void Remove(Source source);
    }





}
