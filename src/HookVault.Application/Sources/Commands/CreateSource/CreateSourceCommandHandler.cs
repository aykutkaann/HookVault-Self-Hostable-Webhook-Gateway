using HookVault.Application.Common.Exceptions;
using HookVault.Domain.Entities;
using HookVault.Domain.Repositories;
using HookVault.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HookVault.Application.Sources.Commands.CreateSource
{
    public sealed class CreateSourceCommandHandler : IRequestHandler<CreateSourceCommand, SourceDto>
    {

        private readonly ISourceRepository _sourceRepository;



        public CreateSourceCommandHandler(ISourceRepository sourceRepository)
        {
            _sourceRepository = sourceRepository;
        }

        public async Task<SourceDto> Handle(CreateSourceCommand request, CancellationToken cancellationToken)
        {

            var existing = await _sourceRepository.GetByNameAsync(request.Name, cancellationToken);

            if (existing is not null)
                throw new ConflictException($"Source {request.Name} already exists.");

            var source = new Source(
                request.Name,
                request.Algorithm,
                request.SignatureHeaderName ?? string.Empty,
                request.SigningSecret ?? string.Empty);


            await _sourceRepository.AddAsync(source, cancellationToken);

            return new SourceDto(source.Id, source.Name,source.SignatureHeaderName,
                source.Algorithm, HasSigningSecret:!string.IsNullOrWhiteSpace(source.SigningSecret),source.CreatedAt );
        }
    }
}
