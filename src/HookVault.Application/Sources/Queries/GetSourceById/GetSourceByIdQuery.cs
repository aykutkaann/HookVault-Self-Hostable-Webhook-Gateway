using HookVault.Application.Abstractions.Messaging;
using HookVault.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace HookVault.Application.Sources.Queries.GetSourceById
{
    public sealed record  GetSourceByIdQuery(Guid Id) : IQuery<SourceDto?>;
    
    
}
