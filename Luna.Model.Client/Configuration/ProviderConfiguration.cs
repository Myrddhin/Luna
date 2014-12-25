using System;

namespace Luna.Model.Configuration
{
    public class ProviderConfiguration
    {
        public Guid Id { get; set; }

        public ProviderTypes ProviderType { get; set; }

        public string Parameters { get; set; }

        public bool Geolocalizer { get; set; }

        public bool Contacts { get; set; }
    }
}