using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HookVault.Application.Endpoints.DeleteEndpoint
{
    public sealed class DeleteEndpointCommandValidator :AbstractValidator<DeleteEndpointCommand>
    {
        public DeleteEndpointCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}
