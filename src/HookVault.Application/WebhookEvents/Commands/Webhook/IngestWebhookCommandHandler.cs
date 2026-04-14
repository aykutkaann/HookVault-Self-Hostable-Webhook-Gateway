using HookVault.Application.Common.Exceptions;
using HookVault.Domain.Entities;
using HookVault.Domain.Repositories;
using MediatR;

namespace HookVault.Application.WebhookEvents.Commands.IngestWebhook;

public sealed class IngestWebhookCommandHandler
    : IRequestHandler<IngestWebhookCommand, IngestWebhookResult>
{
    private static readonly string[] IdempotencyHeaderNames =
    {
        "Idempotency-Key",
        "X-Request-Id",
        "X-Idempotency-Key",
        "Stripe-Idempotency-Key"
    };

    private readonly IEndpointRepository _endpointRepository;
    private readonly IWebhookEventRepository _webhookEventRepository;

    public IngestWebhookCommandHandler(
        IEndpointRepository endpointRepository,
        IWebhookEventRepository webhookEventRepository)
    {
        _endpointRepository = endpointRepository;
        _webhookEventRepository = webhookEventRepository;
    }

    public async Task<IngestWebhookResult> Handle(
        IngestWebhookCommand request,
        CancellationToken cancellationToken)
    {
        var endpoint = await _endpointRepository.GetBySourceAndSlugAsync(
            request.SourceSlug, request.EndpointSlug, cancellationToken);

        if (endpoint is null)
            throw new NotFoundException("Endpoint", $"{request.SourceSlug}/{request.EndpointSlug}");

        if (!endpoint.IsActive)
            throw new EndpointInactiveException(endpoint.Id);

        var idempotencyKey = TryExtractIdempotencyKey(request.Headers);

        if (idempotencyKey is not null)
        {
            var existing = await _webhookEventRepository
                .GetByIdempotencyKeyAsync(idempotencyKey, cancellationToken);

            if (existing is not null)
                return new IngestWebhookResult(existing.Id, Duplicate: true);
        }

        var webhookEvent = WebHookEvent.CreateReceived(
            endpoint.Id,
            request.SourceSlug,
            request.HttpMethod,
            request.Headers,
            request.RawBody,
            request.ContentType,
            idempotencyKey);

        await _webhookEventRepository.AddAsync(webhookEvent, cancellationToken);

        return new IngestWebhookResult(webhookEvent.Id, Duplicate: false);
    }

    private static string? TryExtractIdempotencyKey(IReadOnlyDictionary<string, string> headers)
    {
        foreach (var name in IdempotencyHeaderNames)
        {
            if (headers.TryGetValue(name, out var value) && !string.IsNullOrWhiteSpace(value))
                return value;
        }
        return null;
    }
}
