using FluentValidation;
using HookVault.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace HookVault.Application.Endpoints.CreateEndpoints
{
    public sealed class CreateEndpointCommandValidator :AbstractValidator<CreateEndpointCommand>
    {
        public CreateEndpointCommandValidator()
        {
            RuleFor(x => x.SourceId).NotEmpty();

            RuleFor(x => x.Slug).Matches(@"^[a-z0-9][a-z0-9-]*$").MaximumLength(100);

            RuleFor(x => x.DestinationUrls).NotEmpty().MaximumLength(2048);

            When(x => x.RetryPolicy is not null, () =>
            {
                RuleFor(x => x.RetryPolicy!.MaxRetries)
                 .GreaterThanOrEqualTo(0).WithMessage("MaxRetries must be 0 or greater.");

                RuleFor(x => x.RetryPolicy!.InitialDelaySeconds)
                .GreaterThanOrEqualTo(1).WithMessage("InitialDelaySeconds must be at least 1 second.");

                RuleFor(x => x.RetryPolicy!.BackoffMultiplier)
                    .GreaterThanOrEqualTo(1.0).WithMessage("BackoffMultiplier must be 1.0 or greater.");

                RuleFor(x => x.RetryPolicy!.MaxDelaySeconds)
                    .GreaterThanOrEqualTo(x => x.RetryPolicy!.InitialDelaySeconds)
                    .WithMessage("MaxDelaySeconds must be greater than or equal to InitialDelaySeconds.");
            });

       
        }
    }
}
