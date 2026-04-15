using HookVault.Application.Abstractions.Messaging;
using HookVault.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace HookVault.Application.Endpoints.GetEndpointById
{
    public sealed record GetEndpointByIdQuery(Guid Id) : IQuery<EndpointDto?>;
    
    
}
