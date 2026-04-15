using HookVault.Application.DTOs;
using HookVault.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HookVault.Application.Endpoints.ListEndpointsBySource
{
    public sealed class ListEndpointsBySourceQueryHandler :IRequestHandler<ListEndpointsBySourceQuery, IReadOnlyList<EndpointDto>>
    {
        private readonly IEndpointRepository _endpointRepository;

        public ListEndpointsBySourceQueryHandler(IEndpointRepository endpointRepository)
        {
            _endpointRepository = endpointRepository;
        }

        public async Task<IReadOnlyList<EndpointDto>> Handle(ListEndpointsBySourceQuery request, CancellationToken cancellationToken)
        {
            var endpoints = await _endpointRepository.ListBySourceAsync(request.SourceId, cancellationToken);

            return endpoints.Select(EndpointDto.FromEntity).ToList();
        }
    }
}
