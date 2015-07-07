using System;

namespace Luna.Model.Configuration
{
    public class ExternalAccount
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public bool IsContactDefault { get; set; }

        public bool IsMailDefault { get; set; }

        public ProviderTypes ProviderType { get; set; }

        public string Parameters { get; set; }

        public bool Geolocalizer { get; set; }

        public bool Contacts { get; set; }

        public bool Mail { get; set; }
    }
}