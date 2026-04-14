using HookVault.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace HookVault.Domain.Entities
{
    public class Source
    {
        public Guid Id { get;private set; }
        public string Name { get; private set; } = null!;
        public string SignatureHeaderName { get; private set; } = null!;
        public SignatureAlgorithm Algorithm { get; private set; }
        public string SigningSecret { get; private set; } = null!;
        public DateTime CreatedAt { get; private set; }



        public  ICollection<Endpoint> Endpoints { get; } = new List<Endpoint>();


        private Source() { }

        public Source(string name, SignatureAlgorithm algorithm, string signatureHeaderName, string signingSecret)
        {

            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be empty.", nameof(name));

            if (algorithm != SignatureAlgorithm.None)
            {
                if (string.IsNullOrWhiteSpace(signatureHeaderName))
                    throw new ArgumentException("SignatureHeaderName is required for this algorithm.", nameof(signatureHeaderName));

                if (string.IsNullOrWhiteSpace(signingSecret))
                    throw new ArgumentException("SigningSecret is required for this algorithm.", nameof(signingSecret));
            }

            Id = Guid.NewGuid();
            Name = name.ToLowerInvariant().Trim();
            Algorithm = algorithm;
            SignatureHeaderName = signatureHeaderName;
            SigningSecret = signingSecret;
            CreatedAt = DateTime.UtcNow;
        }


    
        //Domain Methods
        public void Rename(string newName)
        {

            if (string.IsNullOrWhiteSpace(newName))
                throw new ArgumentException("New name cannot be empty");

            Name = newName.ToUpperInvariant().Trim();

        }

        public void RotateSecret(string newSecret)
        {

            if (Algorithm != SignatureAlgorithm.None && string.IsNullOrWhiteSpace(newSecret))
                throw new ArgumentException("Secret cannot be empty when an algorithm is set.");
            SigningSecret = newSecret;
        }

        public void UpdateSignatureConfig(SignatureAlgorithm algorithm, string headerName)
        {

            SignatureHeaderName = headerName;

            if (algorithm != SignatureAlgorithm.None && string.IsNullOrWhiteSpace(headerName))
                throw new ArgumentException("Header name is required for the selected algorithm.");

            Algorithm = algorithm;
        }


    }
}
