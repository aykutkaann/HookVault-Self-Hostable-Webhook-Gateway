using HookVault.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace HookVault.Infrastructure.Persistence.Configuration
{
    public sealed class DeliveryAttemptConfiguration : IEntityTypeConfiguration<DeliveryAttempt>
    {
        public void Configure(EntityTypeBuilder<DeliveryAttempt> builder)
        {
            builder.ToTable("delivery_attempts");

            builder.HasKey(d => d.Id);
            builder.Property(d => d.Id).ValueGeneratedNever();

            builder.Property(d => d.WebhookEventId).IsRequired();

            builder.HasIndex(a => a.WebhookEventId)
                .HasDatabaseName("ix_delivery_attempts_webhook_event_id");

            builder.Property(d => d.AttemptNumber).IsRequired();

            builder.Property(d => d.RequestedAt).IsRequired();

            builder.Property(d => d.ResponseStatusCode);

            builder.Property(d => d.ResponseBody).HasColumnType("text");

            builder.Property(d => d.ResponseTimeMs).IsRequired();

            builder.Property(d => d.ErrorMessage).HasMaxLength(2000);

            builder.Property(d => d.NextTryAt);

            builder.Property(d => d.IsSuccess).IsRequired();








            builder.HasOne(d => d.WebHookEvent)
                .WithMany(w => w.DeliveryAttempts)
                .HasForeignKey(d => d.WebhookEventId)
                .OnDelete(DeleteBehavior.Cascade);
                

        }
    }
}
