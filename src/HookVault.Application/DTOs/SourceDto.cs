using HookVault.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace HookVault.Infrastructure.DTOs
{
    public sealed record SourceDto(
        Guid Id,
        string Name,
        string SignatureHeaderName,
        SignatureAlgorithm Algorithm,
        bool HasSigningSecret,
        DateTime CreatedAt);
}
