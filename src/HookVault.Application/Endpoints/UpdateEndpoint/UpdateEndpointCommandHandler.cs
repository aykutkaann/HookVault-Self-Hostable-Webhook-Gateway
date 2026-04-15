using HookVault.Application.DTOs;
using HookVault.Application.Common.Exceptions;
using HookVault.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using HookVault.Domain.ValueObjects;

namespace HookVault.Application.Endpoints.UpdateEndpoint
{
    public sealed class UpdateEndpointCommandHandler :IRequestHandler<UpdateEndpointCommand, EndpointDto>
    {

        private readonly IEndpointRepository _endpointRepository;
        

        public UpdateEndpointCommandHandler(IEndpointRepository endpointRepository)
        {
            _endpointRepository = endpointRepository;
        }

        public async Task<EndpointDto> Handle(UpdateEndpointCommand request, CancellationToken cancellationToken)
        {
            var endpoint = await _endpointRepository.GetByIdAsync(request.Id, cancellationToken);

            if (endpoint is null)
                throw new NotFoundException("Endpoint not found.", request.Id);

            endpoint.UpdateDestination(request.DestinationUrls);
            
            if(request.IsActive == true)
            {
                endpoint.Activate();
            }
            else
            {
                endpoint.Deactivate();
            }


            var retryPolicy = request.RetryPolicy is not null ?
                new RetryPolicy(request.RetryPolicy.MaxRetries,
                request.RetryPolicy.InitialDelaySeconds, request.RetryPolicy.BackoffMultiplier,
                request.RetryPolicy.MaxDelaySeconds) : RetryPolicy.Default;

            if(request.RetryPolicy is not null)
            {
                endpoint.UpdateRetryPolicy(retryPolicy);
            }

            _endpointRepository.Update(endpoint);

            return EndpointDto.FromEntity(endpoint);
        }

    }
}
