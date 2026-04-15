using HookVault.Application.DTOs;
using HookVault.Application.Common.Exceptions;
using HookVault.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using HookVault.Domain.ValueObjects;
using HookVault.Domain.Entities;

namespace HookVault.Application.Endpoints.CreateEndpoints
{
    public sealed class CreateEndpointCommandHandler :IRequestHandler<CreateEndpointCommand, EndpointDto>
    {
        private readonly IEndpointRepository _endpointRepository;
        private readonly ISourceRepository _sourceRepository;

        public CreateEndpointCommandHandler(IEndpointRepository endpointRepository, ISourceRepository sourceRepository)
        {
            _endpointRepository = endpointRepository;
            _sourceRepository = sourceRepository;
        }

        public async Task<EndpointDto> Handle(CreateEndpointCommand request, CancellationToken cancellationToken)
        {
            var source = await _sourceRepository.GetByIdAsync(request.SourceId, cancellationToken);

            if (source is null)
                throw new NotFoundException("Source is not found", request.SourceId);

            var existingEndpoint = await _endpointRepository.GetBySourceAndSlugAsync(source.Name, request.Slug, cancellationToken);

            if (existingEndpoint is not null)
                throw new ConflictException($"An endpoint with slug {request.Slug} already exist for source {source.Name}");

            var retryPolicy = request.RetryPolicy is not null ?
                new RetryPolicy(request.RetryPolicy.MaxRetries,
                request.RetryPolicy.InitialDelaySeconds, request.RetryPolicy.BackoffMultiplier, 
                request.RetryPolicy.MaxDelaySeconds) : RetryPolicy.Default;

            var endpoint = new Endpoint(
                source.Id,
                request.Slug,
                request.DestinationUrls,
                retryPolicy);

            await _endpointRepository.AddAsync(endpoint, cancellationToken);

            return EndpointDto.FromEntity(endpoint);

        }

        
    }
}
