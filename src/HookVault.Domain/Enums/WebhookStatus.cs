using System;
using System.Collections.Generic;
using System.Text;

namespace HookVault.Domain.Enums
{
    public enum WebhookStatus
    {
        Received = 0,
        Queued = 1,
        Delivering = 2,
        Delivered = 3,
        Failed = 4,
        DeadLettered = 5,
        Rejected = 6
    }
}
