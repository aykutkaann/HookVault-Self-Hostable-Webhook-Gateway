using HookVault.Application.Abstractions.Messaging;
using HookVault.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace HookVault.Application.Sources.Queries.ListSources
{
    public sealed record ListSourcesQuery : IQuery<IReadOnlyList<SourceDto>>;
    
    
}
