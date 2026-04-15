using HookVault.Application.Abstractions.Messaging;
using HookVault.Application.DTOs;
using HookVault.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace HookVault.Application.Sources.Commands.UpdateSource
{

    public sealed record class UpdateSourceCommand(Guid Id, string Name, SignatureAlgorithm Algorithm, 
        string? SignatureHeaderName) : ICommand<SourceDto>;
}
