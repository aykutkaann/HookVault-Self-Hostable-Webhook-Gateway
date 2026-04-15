using HookVault.Application.DTOs;
using HookVault.Domain.Repositories;
using MediatR;


namespace HookVault.Application.Sources.Queries.GetSourceById
{
    public sealed class GetSourceByIdQueryHandler :IRequestHandler<GetSourceByIdQuery, SourceDto?>
    {
        private readonly ISourceRepository _sourceRepository;


        public GetSourceByIdQueryHandler(ISourceRepository sourceRepository)
        {
            _sourceRepository = sourceRepository;
        }

        public async Task<SourceDto?> Handle(GetSourceByIdQuery request, CancellationToken cancellationToken)
        {
            var source = await _sourceRepository.GetByIdAsync(request.Id, cancellationToken);
            if (source is null)
            {
                return null;
            }
                


            return new SourceDto(source.Id, source.Name, source.SignatureHeaderName,
                source.Algorithm, HasSigningSecret: !string.IsNullOrWhiteSpace(source.SigningSecret), source.CreatedAt);
        }

    }
}
