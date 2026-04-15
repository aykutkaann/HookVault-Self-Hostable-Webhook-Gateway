using HookVault.Domain.Repositories;
using HookVault.Application.Common.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HookVault.Application.Sources.Commands.DeleteSource
{
    public sealed class DeleteSourceCommandHandler :IRequestHandler<DeleteSourceCommand>
    {
        private readonly ISourceRepository _sourceRepository;
        private readonly IEndpointRepository _endpointRepository;

        public DeleteSourceCommandHandler(ISourceRepository sourceRepository, IEndpointRepository endpointRepository)
        {
            _sourceRepository = sourceRepository;
            _endpointRepository = endpointRepository;
        }

        public async Task Handle(DeleteSourceCommand request, CancellationToken cancellationToken)
        {
            var source = await _sourceRepository.GetByIdAsync(request.Id, cancellationToken);

            if (source is null)
                throw new NotFoundException("Source not found.", request.Id);

            var hasEndpoints = (await _endpointRepository.ListBySourceAsync(source.Id, cancellationToken)).Count > 0;
            if (hasEndpoints)
                throw new ConflictException($"Source {source.Name} has endpoints. Delete them before deleting the source.");

            _sourceRepository.Remove(source);

            return;
        }

    }
}
