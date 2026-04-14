using System;
using System.Collections.Generic;
using System.Text;

namespace HookVault.Infrastructure.Abstractions
{
    public interface IDateTimeProvider
    {
        DateTime UtcNow { get; }
    }
}
