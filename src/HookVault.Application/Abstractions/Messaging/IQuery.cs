using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace HookVault.Application.Abstractions.Messaging
{
    [SuppressMessage("Design", "CA1040:Avoid empty interfaces",
    Justification = "Marker interface used to distinguish queries from commands in the MediatR pipeline.")]
    public interface IQuery<TResponse> : MediatR.IRequest<TResponse>;


}
