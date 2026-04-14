using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace HookVault.Application.Abstractions.Messaging
{
    [SuppressMessage("Design", "CA1040:Avoid empty interfaces",
    Justification = "Marker interface used to tag commands for MediatR pipeline behaviors (validation, unit of work).")]
    public interface ICommand : MediatR.IRequest;

    [SuppressMessage("Design", "CA1040:Avoid empty interfaces",
    Justification = "Marker interface used to tag commands for MediatR pipeline behaviors (validation, unit of work).")]
    public interface ICommand<TResponse> : MediatR.IRequest<TResponse>;



}
