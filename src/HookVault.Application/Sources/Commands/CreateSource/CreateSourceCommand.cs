using HookVault.Domain.Enums;
using HookVault.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using HookVault.Application.Abstractions.Messaging;


namespace HookVault.Application.Sources.Commands.CreateSource
{
    public sealed record class CreateSourceCommand(string Name, SignatureAlgorithm Algorithm,
        string? SignatureHeaderName, string? SigningSecret) : ICommand<SourceDto>;
    
    
}
