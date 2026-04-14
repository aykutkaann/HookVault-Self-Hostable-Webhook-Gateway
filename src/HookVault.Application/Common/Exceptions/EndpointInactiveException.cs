using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics.CodeAnalysis;



namespace HookVault.Application.Common.Exceptions
{
    [SuppressMessage("Design", "CA1032:Implement standard exception constructors",
    Justification = "These exceptions are only constructed with required context — parameterless/message-only constructors would allow invalid states.")]
    public sealed class EndpointInactiveException: Exception
    {

        public EndpointInactiveException(Guid endpointId) : base($"{endpointId} is inactive and cannot receive webhooks.")
        {
            EndpointId = endpointId;
        }

        public Guid EndpointId { get;  }
    }
}
