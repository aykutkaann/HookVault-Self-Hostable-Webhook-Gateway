

namespace HookVault.Domain.ValueObjects
{


    public record RetryPolicy
    {
        public int MaxRetries { get; }
        public int InitialDelaySeconds { get; }
        public double BackoffMultiplier { get; }
        public int MaxDelaySeconds { get; }

        public static RetryPolicy Default => new(5, 5, 3.0, 3600);

        public RetryPolicy(int maxRetries, int initialDelaySeconds, double backoffMultiplier, int maxDelaySeconds)
        {
            // Self-validation 
            if (maxRetries is < 0 or > 20)
                throw new ArgumentException("MaxRetries must be between 0 and 20.", nameof(maxRetries));

            if (initialDelaySeconds < 1)
                throw new ArgumentException("InitialDelaySeconds must be at least 1.", nameof(initialDelaySeconds));

            if (backoffMultiplier < 1.0)
                throw new ArgumentException("BackoffMultiplier cannot be less than 1.0.", nameof(backoffMultiplier));

            if (maxDelaySeconds < initialDelaySeconds)
                throw new ArgumentException("MaxDelaySeconds must be greater than or equal to InitialDelaySeconds.", nameof(maxDelaySeconds));

            MaxRetries = maxRetries;
            InitialDelaySeconds = initialDelaySeconds;
            BackoffMultiplier = backoffMultiplier;
            MaxDelaySeconds = maxDelaySeconds;
        }

        public TimeSpan DelayForAttempt(int attemptNumber)
        {
            if (attemptNumber <= 0) return TimeSpan.Zero;


            double delayInSeconds = InitialDelaySeconds * Math.Pow(BackoffMultiplier, attemptNumber - 1);

 
            double finalDelay = Math.Min(MaxDelaySeconds, delayInSeconds);

            return TimeSpan.FromSeconds(finalDelay);
        }
    }

}
