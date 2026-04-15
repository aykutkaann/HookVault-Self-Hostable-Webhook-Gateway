using HookVault.Application.DTOs;
using HookVault.Domain.Entities;
using HookVault.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HookVault.Application.Sources.Queries.ListSources
{
    public sealed class ListSourcesQueryHandler :IRequestHandler<ListSourcesQuery, IReadOnlyList<SourceDto>>
    {
        private readonly ISourceRepository _sourceRepository;

        public ListSourcesQueryHandler(ISourceRepository sourceRepository)
        {
            _sourceRepository = sourceRepository;
        }

        public async Task<IReadOnlyList<SourceDto>> Handle(ListSourcesQuery request, CancellationToken cancellationToken)
        {

            var sources = await _sourceRepository.ListAsync(cancellationToken);

            return sources.Select(SourceDto.FromEntity).ToList();
        }
    }
}
