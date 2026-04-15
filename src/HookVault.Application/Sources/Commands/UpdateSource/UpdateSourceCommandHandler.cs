using HookVault.Application.DTOs;
using HookVault.Domain.Entities;
using HookVault.Domain.Repositories;
using HookVault.Application.Common.Exceptions;
using MediatR;


namespace HookVault.Application.Sources.Commands.UpdateSource
{
    public sealed class UpdateSourceCommandHandler :IRequestHandler<UpdateSourceCommand, SourceDto>
    {
        private readonly ISourceRepository _sourceRepository;



        public UpdateSourceCommandHandler(ISourceRepository sourceRepository)
        {
            _sourceRepository = sourceRepository;
        }

        public async Task<SourceDto> Handle(UpdateSourceCommand request, CancellationToken cancellationToken)
        {
            var source = await _sourceRepository.GetByIdAsync(request.Id, cancellationToken);

            if (source is null)
                throw new NotFoundException("Source", request.Id);

            if(request.Name != source.Name)
            {
                var existing = await _sourceRepository.GetByNameAsync(request.Name, cancellationToken);

                if (existing is not null && existing.Id != source.Id)
                    throw new ConflictException($"{request.Name} already exists.");
            }


            source.Rename(request.Name);

            source.UpdateSignatureConfig(request.Algorithm, request.SignatureHeaderName ?? string.Empty);

             _sourceRepository.Update(source);


            return new SourceDto(source.Id, source.Name, source.SignatureHeaderName,
                source.Algorithm, HasSigningSecret: !string.IsNullOrWhiteSpace(source.SigningSecret), source.CreatedAt);

        }
    }
}
