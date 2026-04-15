using HookVault.Domain.Entities;
using HookVault.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace HookVault.Application.DTOs
{
    public sealed record SourceDto(
        Guid Id,
        string Name,
        string SignatureHeaderName,
        SignatureAlgorithm Algorithm,
        bool HasSigningSecret,
        DateTime CreatedAt)
    {
        public static SourceDto FromEntity(Source source) =>
            new(
                source.Id,
                source.Name,
                source.SignatureHeaderName,
                source.Algorithm,
                !string.IsNullOrWhiteSpace(source.SigningSecret),
                source.CreatedAt);
    }
}
