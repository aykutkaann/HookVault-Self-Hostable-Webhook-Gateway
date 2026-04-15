using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HookVault.Application.Sources.Commands.UpdateSource
{
    public sealed class UpdateSourceCommandValidator :AbstractValidator<UpdateSourceCommand>
    {

        public UpdateSourceCommandValidator()
        {
            RuleFor(s => s.Id).NotEmpty();

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Source name is required.")
                .MaximumLength(100).WithMessage("Source name cannot exceed 100 characters.")
                .Matches(@"^[a-z0-9][a-z0-9-]*$")
                .WithMessage("Source name must be lowercase, alphanumeric, and may contain hyphens (cannot start with a hyphen).");

            RuleFor(s => s.Algorithm).IsInEnum();

            When(s => s.Algorithm != Domain.Enums.SignatureAlgorithm.None, () =>
            {

                RuleFor(s => s.SignatureHeaderName).NotEmpty();
            });
        }
    }
}
