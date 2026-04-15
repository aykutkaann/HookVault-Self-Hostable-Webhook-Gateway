using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HookVault.Application.Sources.Commands.CreateSource
{
    public  sealed  class CreateSourceCommandValidator :AbstractValidator<CreateSourceCommand>
    {
        public CreateSourceCommandValidator()
        {

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Source name is required.")
                .MaximumLength(100).WithMessage("Source name cannot exceed 100 characters.")
                .Matches(@"^[a-z0-9][a-z0-9-]*$")
                .WithMessage("Source name must be lowercase, alphanumeric, and may contain hyphens (cannot start with a hyphen).");

            RuleFor(s => s.Algorithm)
                .IsInEnum().WithMessage("A valid signature algorithm must be selected.");

            When(s => s.Algorithm != Domain.Enums.SignatureAlgorithm.None, () =>
            {
                RuleFor(s => s.SignatureHeaderName)
                    .NotEmpty().MaximumLength(200);

                RuleFor(s => s.SigningSecret)
                    .NotEmpty().MinimumLength(16);
            });
        }

    }
}
