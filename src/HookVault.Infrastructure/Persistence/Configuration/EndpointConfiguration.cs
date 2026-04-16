using HookVault.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace HookVault.Infrastructure.Persistence.Configuration
{
    public sealed class EndpointConfiguration :IEntityTypeConfiguration<Endpoint>
    {
        public void Configure(EntityTypeBuilder<Endpoint> builder)
        {
            builder.ToTable("endpoints");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id).ValueGeneratedNever();

            builder.Property(e => e.SourceId).IsRequired();

            builder.Property(e => e.Slug).IsRequired().HasMaxLength(100);

            builder.Property(e => e.DestinationUrls).IsRequired().HasMaxLength(2048);

            builder.Property(e => e.IsActive).IsRequired();

            builder.Property(e => e.CreatedAt).IsRequired();
            builder.Property(e => e.UpdatedAt);

            builder.HasIndex(e => new { e.SourceId, e.Slug }).IsUnique();

            //RetryPolicy is value object. Not an entity
            builder.OwnsOne(e => e.Policy, policy =>
            {
                policy.Property(p => p.MaxRetries)
                      .HasColumnName("retry_max_retries")
                      .IsRequired();

                policy.Property(p => p.InitialDelaySeconds)
                      .HasColumnName("retry_initial_delay_seconds")
                      .IsRequired();

                policy.Property(p => p.BackoffMultiplier)
                      .HasColumnName("retry_backoff_multiplier")
                      .IsRequired();

                policy.Property(p => p.MaxDelaySeconds)
                      .HasColumnName("retry_max_delay_seconds")
                      .IsRequired();
            });

        }
    }
}
