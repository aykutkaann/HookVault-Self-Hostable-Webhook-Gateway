using HookVault.Application.Abstractions.Messaging;
using HookVault.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace HookVault.Application.Endpoints.ListEndpointsBySource
{
    public sealed record ListEndpointsBySourceQuery(Guid SourceId) : IQuery<IReadOnlyList<EndpointDto>>;
    
    
}
