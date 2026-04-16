using HookVault.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace HookVault.Infrastructure.Persistence.Configuration
{
    public sealed class SourceConfiguration :IEntityTypeConfiguration<Source>
    {
        public void Configure(EntityTypeBuilder<Source> builder)
        {
            builder.ToTable("sources");

            builder.HasKey(s => s.Id);

            builder.Property(s => s.Id).ValueGeneratedNever();

            builder.Property(s => s.Name).IsRequired().HasMaxLength(100);

            builder.Property(s => s.SignatureHeaderName).IsRequired().HasMaxLength(200);

            builder.Property(s => s.Algorithm).HasConversion<string>().HasMaxLength(20);

            builder.Property(s => s.SigningSecret).IsRequired().HasMaxLength(512);

            builder.Property(s => s.CreatedAt).IsRequired();

            builder.HasIndex(s => s.Name).IsUnique();

            builder.HasMany(s => s.Endpoints)
                .WithOne(e => e.Source)
                .HasForeignKey(s => s.SourceId)
                .OnDelete(DeleteBehavior.Restrict); //you cannot delete a source that still has endpoints.(DeleteSourceCommandHandler.cs)
        }
    }
}
