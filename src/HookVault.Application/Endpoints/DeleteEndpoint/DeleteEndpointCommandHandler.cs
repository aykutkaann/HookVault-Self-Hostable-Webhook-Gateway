using HookVault.Domain.Repositories;
using HookVault.Application.Common.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HookVault.Application.Endpoints.DeleteEndpoint
{
    public sealed class DeleteEndpointCommandHandler :IRequestHandler<DeleteEndpointCommand>
    {
        private readonly IEndpointRepository _endpointRepository;

        public DeleteEndpointCommandHandler(IEndpointRepository endpointRepository)
        {
            _endpointRepository = endpointRepository;
        }

        public async Task Handle(DeleteEndpointCommand request, CancellationToken cancellationToken)
        {
            var endpoint = await _endpointRepository.GetByIdAsync(request.Id, cancellationToken);
            if (endpoint is null)
                throw new NotFoundException("Endpoint is null", request.Id);

            _endpointRepository.Remove(endpoint);
        }
    }
}
