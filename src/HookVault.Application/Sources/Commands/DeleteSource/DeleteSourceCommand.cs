using HookVault.Application.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace HookVault.Application.Sources.Commands.DeleteSource
{

    public sealed record class DeleteSourceCommand(Guid Id) : ICommand;
}
