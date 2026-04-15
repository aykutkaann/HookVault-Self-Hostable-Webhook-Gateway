using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HookVault.Application.Sources.Commands.DeleteSource
{
    public sealed class DeleteSourceCommandValidator: AbstractValidator<DeleteSourceCommand>
    {
        public DeleteSourceCommandValidator()
        {
            RuleFor(s => s.Id).NotEmpty();
        }
    }
}
