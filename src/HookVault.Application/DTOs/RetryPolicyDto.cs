using System;
using System.Collections.Generic;
using System.Text;

namespace HookVault.Infrastructure.DTOs
{

    public sealed record RetryPolicyDto(int MaxRetries, int InitialDelaySeconds, double BackoffMultiplier, int MaxDelaySeconds);

}
