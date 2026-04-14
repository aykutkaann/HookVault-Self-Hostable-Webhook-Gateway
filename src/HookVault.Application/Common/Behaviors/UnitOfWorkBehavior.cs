using ICommandGeneric = HookVault.Application.Abstractions.Messaging.ICommand<object>;
using HookVault.Application.Abstractions.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;


namespace HookVault.Application.Common.Behaviors
{
    public sealed class UnitOfWorkBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
    {
        private readonly IUnitOfWork _unitOfWork;


        public UnitOfWorkBehavior(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var response = await next(cancellationToken);

            if (IsCommand(request))
                await _unitOfWork.SaveChangesAsync(cancellationToken);

            return response;
        }
        private static bool IsCommand(TRequest request) =>
         request is ICommand || request.GetType().GetInterfaces()
             .Any(i => i.IsGenericType &&
                  i.GetGenericTypeDefinition() == typeof(HookVault.Application.Abstractions.Messaging.ICommand<>));

    }
}
