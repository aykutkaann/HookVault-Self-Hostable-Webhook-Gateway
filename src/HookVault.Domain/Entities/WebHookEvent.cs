using HookVault.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;

namespace HookVault.Domain.Entities
{
    public class WebHookEvent
    {
        public Guid Id { get; private set; }
        public Guid EndpointId { get; private set; }
        public  Endpoint Endpoint { get; private set; } = null!;



        public string SourceName { get; private set; } = null!;
        public string HttpMethod { get; private set; } = null!;
        public IReadOnlyDictionary<string, string> Headers { get; private set; } = null!;
        public string RawBody { get; private set; } = null!;
        public string ContentType { get; private set; } = null!;


        public SignatureStatus StatusSignature  { get; private set; }
        public string? IdempotencyKey { get; private set; }
        public WebhookStatus StatusWebhook { get; private set; }


        public DateTime ReceivedAt { get; private set; }
        public DateTime? DeliveredAt { get; private set; }


        public ICollection<DeliveryAttempt> DeliveryAttempts { get; } = new List<DeliveryAttempt>();


        private WebHookEvent() { }

        public static WebHookEvent CreateReceived(
            Guid endpointId, string sourceName, string httpMethod,
            IReadOnlyDictionary<string,string> headers, string rawBody, 
            string contentType,
            string? idempotencyKey = null)
        {

            return new WebHookEvent
            {
                Id = Guid.NewGuid(),
                EndpointId = endpointId,
                SourceName = sourceName,
                HttpMethod = httpMethod,
                Headers = headers ?? new Dictionary<string, string>(),
                RawBody = rawBody,
                ContentType = contentType,
                IdempotencyKey = idempotencyKey,
                StatusWebhook = WebhookStatus.Received,
                StatusSignature = SignatureStatus.SKIPPED,
                ReceivedAt = DateTime.UtcNow

            };

        }


        public void MarkQueued()
        {

            EnsureStatus(WebhookStatus.Received, WebhookStatus.Failed);
            StatusWebhook = WebhookStatus.Queued;
        }

        public void MarkDelivering()
        {
            EnsureStatus(WebhookStatus.Queued);
            StatusWebhook = WebhookStatus.Delivering;
        }

        public void MarkDelivered()
        {
            EnsureStatus(WebhookStatus.Delivering, WebhookStatus.Queued);
            StatusWebhook = WebhookStatus.Delivered;
            DeliveredAt = DateTime.UtcNow;
        }

        public void MarkFailed()
        {

            EnsureStatus(WebhookStatus.Delivering);
            StatusWebhook = WebhookStatus.Failed;
        }

        public void MarkDeadLettered()
        {
            EnsureStatus(WebhookStatus.Failed);
            StatusWebhook = WebhookStatus.DeadLettered;

        }
        public void MarkRejected()
        {
            EnsureStatus(WebhookStatus.Received);
            StatusWebhook = WebhookStatus.Rejected;

        }
        public void SetSignatureStatus(SignatureStatus status)
        {
            StatusSignature = status;

        }


        //Private helper

        private void EnsureStatus(params WebhookStatus[] validStates)
        {
            if (!validStates.Contains(StatusWebhook))
            {
                throw new InvalidOperationException(
                    $"Invalid state transition from {StatusWebhook} to target state. Allowed states: {string.Join(", ", validStates)}");
            }

        }







    }
}
