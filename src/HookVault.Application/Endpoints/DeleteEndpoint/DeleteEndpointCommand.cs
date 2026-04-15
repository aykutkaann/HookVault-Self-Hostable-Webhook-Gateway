using HookVault.Application.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace HookVault.Application.Endpoints.DeleteEndpoint
{
    public sealed record DeleteEndpointCommand(Guid Id) : ICommand;
    
    
}
