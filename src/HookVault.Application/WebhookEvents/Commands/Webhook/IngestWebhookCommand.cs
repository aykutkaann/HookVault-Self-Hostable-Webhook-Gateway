using HookVault.Application.Abstractions.Messaging;

namespace HookVault.Application.WebhookEvents.Commands.IngestWebhook;

public sealed record IngestWebhookCommand(
    string SourceSlug,
    string EndpointSlug,
    string HttpMethod,
    IReadOnlyDictionary<string, string> Headers,
    string RawBody,
    string ContentType
) : ICommand<IngestWebhookResult>;

public sealed record IngestWebhookResult(Guid WebhookEventId, bool Duplicate);
