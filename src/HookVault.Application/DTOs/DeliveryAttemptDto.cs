using System;
using System.Collections.Generic;
using System.Text;

namespace HookVault.Infrastructure.DTOs
{

    public sealed record DeliveryAttemptDto(
        Guid Id,
        int AttemptNumber,
        DateTime RequestedAt,
        int? ResponseStatusCode,
        string? ResponseBody,
        long ResponseTimeMs,
        string? ErrorMessage,
        DateTime? NextRetryAt,
        bool Success);
}
