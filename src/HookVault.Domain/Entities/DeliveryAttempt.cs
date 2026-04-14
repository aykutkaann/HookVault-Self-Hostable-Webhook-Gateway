using System;
using System.Collections.Generic;
using System.Text;

namespace HookVault.Domain.Entities
{
    public class DeliveryAttempt
    {
        public Guid Id { get; private set; }
        public Guid WebhookEventId { get; private set; }
        public  WebHookEvent WebHookEvent { get; private set; } = null!;

        public int AttemptNumber { get; private set; } 
        public DateTime RequestedAt { get; private set; }
        public int? ResponseStatusCode { get; private set; }
        public string? ResponseBody { get; private set; }
        public long ResponseTimeMs { get; private set; }
        public string? ErrorMessage { get; private set; } = null!;
        public DateTime? NextTryAt { get; private set; }
        public bool IsSuccess { get; private set; }

        private DeliveryAttempt() { }


        public DeliveryAttempt(Guid webhookEventId, int attemptNumber, int? responseStatusCode,
            string? body, long responseTimeMs, string? errorMessage, DateTime? nextTryAt, bool isSuccess)
        {
            Id = Guid.NewGuid();
            WebhookEventId = webhookEventId;
            AttemptNumber = attemptNumber;
            ResponseStatusCode = responseStatusCode;
            ResponseBody = body;
            ResponseTimeMs = responseTimeMs;
            ErrorMessage = errorMessage;
            IsSuccess = isSuccess;
            RequestedAt = DateTime.UtcNow;
            NextTryAt = nextTryAt;

            ResponseBody = TruncateBody(body, 4096);
        }



        public static DeliveryAttempt CreateSuccess(
            Guid webhookEventId,
            int attemptNumber,
            int statusCode,
            string? body,
            long responseTimeMs)
        {
            return new DeliveryAttempt(
                webhookEventId,
                attemptNumber,
                statusCode,
                body,
                responseTimeMs,
                null,
                null,
                true);
            

            
        }

        public static DeliveryAttempt CreateFailure(
            Guid webhookEventId,
            int attemptNumber,
            int? statusCode,
            string? body,
            long responseTimeMs,
            string? errorMessage,
            DateTime? nextRetryAt)
            {
                return new DeliveryAttempt(
                    webhookEventId,
                    attemptNumber,
                    statusCode,
                    body,
                    responseTimeMs,
                    errorMessage,
                    nextRetryAt,
                    false 
                );
            }


        private static string? TruncateBody(string? body, int maxLength)
        {
            if (string.IsNullOrWhiteSpace(body) || body.Length <= maxLength)
                return body;

            return body[..maxLength]; 
        }

    }
}
