using HookVault.Application.Abstractions.Messaging;
using HookVault.Application.DTOs;
using HookVault.Domain.ValueObjects;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HookVault.Application.Endpoints.CreateEndpoints
{
    public sealed record CreateEndpointCommand(Guid SourceId, string Slug, string DestinationUrls,
        RetryPolicyDto? RetryPolicy = null) : ICommand<EndpointDto>;
    
    
}
