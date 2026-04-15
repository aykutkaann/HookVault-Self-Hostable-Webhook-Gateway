using HookVault.Application.Common.Exceptions;
using HookVault.Application.DTOs;
using HookVault.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HookVault.Application.Endpoints.GetEndpointById
{
    public sealed class GetEndpointByIdQueryHandler :IRequestHandler<GetEndpointByIdQuery, EndpointDto?>
    {
        private readonly IEndpointRepository _endpointRepository;

        public GetEndpointByIdQueryHandler(IEndpointRepository endpointRepository)
        {
            _endpointRepository = endpointRepository;
        }

        public async Task<EndpointDto?> Handle(GetEndpointByIdQuery request, CancellationToken cancellationToken)
        {
            var endpoint = await _endpointRepository.GetByIdAsync(request.Id, cancellationToken);
            if (endpoint is null)
                return null;

            return EndpointDto.FromEntity(endpoint);
        }
    }
}
