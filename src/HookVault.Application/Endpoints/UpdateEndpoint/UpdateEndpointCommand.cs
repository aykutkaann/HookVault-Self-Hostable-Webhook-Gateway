using HookVault.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HookVault.Application.Endpoints.UpdateEndpoint
{
    public sealed record  UpdateEndpointCommand(Guid Id, string DestinationUrls, bool IsActive,
        RetryPolicyDto? RetryPolicy= null): IRequest<EndpointDto>
    {
    }
}
