using HookVault.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HookVault.Infrastructure.Persistence
{
    public class HookVaultDbContext(DbContextOptions<HookVaultDbContext> options) :DbContext(options)
    {

        public DbSet<Source> Sources => Set<Source>();
        public DbSet<Endpoint> Endpoints => Set<Endpoint>();
        public DbSet<WebHookEvent> WebHookEvents => Set<WebHookEvent>();
        public DbSet<DeliveryAttempt> DeliveryAttempts => Set<DeliveryAttempt>();


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(HookVaultDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }

    }
}
