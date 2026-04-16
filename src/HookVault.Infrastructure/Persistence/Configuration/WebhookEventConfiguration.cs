using HookVault.Domain.Entities;
using HookVault.Infrastructure.Persistence.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace HookVault.Infrastructure.Persistence.Configuration
{
    public sealed class WebhookEventConfiguration: IEntityTypeConfiguration<WebHookEvent>
    {
        public void Configure(EntityTypeBuilder<WebHookEvent> builder)
        {
            builder.ToTable("webhook_events");

            builder.HasKey(w => w.Id);
            builder.Property(w => w.Id).ValueGeneratedNever();

            builder.HasIndex(w => new { w.EndpointId, w.ReceivedAt })
             .HasDatabaseName("ix_webhook_events_endpoint_received");

            builder.HasIndex(w => w.StatusWebhook)
                   .HasDatabaseName("ix_webhook_events_status");

            builder.HasIndex(w => w.IdempotencyKey)
                   .IsUnique()
                   .HasFilter("\"IdempotencyKey\" IS NOT NULL")
                   .HasDatabaseName("ix_webhook_events_idempotency_key");

            builder.Property(w => w.EndpointId).IsRequired();

            builder.Property(w => w.SourceName).IsRequired().HasMaxLength(100);

            builder.Property(w => w.HttpMethod).IsRequired().HasMaxLength(10);

            builder.Property(w => w.ContentType).IsRequired().HasMaxLength(200);

            builder.Property(w => w.RawBody).IsRequired().HasColumnType("text");

            builder.Property(w => w.StatusSignature).HasConversion<string>().HasMaxLength(20);

            builder.Property(w => w.StatusWebhook).HasConversion<string>().HasMaxLength(20);

            builder.Property(w => w.IdempotencyKey).HasMaxLength(200);

            builder.Property(w => w.ReceivedAt).IsRequired();

            builder.Property(w => w.DeliveredAt);

            builder.Property(e => e.Headers)
                   .HasConversion(new DictionaryJsonConverter())
                   .HasColumnType("jsonb")
                   .IsRequired();


            builder.HasOne(w => w.Endpoint)
                .WithMany()
                .HasForeignKey(w => w.EndpointId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
