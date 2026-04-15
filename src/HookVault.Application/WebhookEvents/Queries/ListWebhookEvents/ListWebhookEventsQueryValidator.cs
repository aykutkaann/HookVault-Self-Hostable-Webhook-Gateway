using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HookVault.Application.WebhookEvents.Queries.ListWebhookEvents
{
    public sealed class ListWebhookEventsQueryValidator :AbstractValidator<ListWebhookEventsQuery>
    {
        public ListWebhookEventsQueryValidator()
        {
            RuleFor(x => x.Page).GreaterThanOrEqualTo(1);
            RuleFor(x => x.PageSize).GreaterThanOrEqualTo(1).LessThanOrEqualTo(200);


            RuleFor(x => x.From).LessThan(x => x.To!.Value).When(x => x.From.HasValue && x.To.HasValue);


        }
    }
}
