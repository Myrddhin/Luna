using System;

namespace Luna.Model.Cloud.Meta
{
    public class Repository : ClientEntity
    {
        public string Name { get; set; }

        public string Parameters { get; set; }

        public decimal SchemaVersion { get; set; }

        public Guid SubscriptionId { get; set; }
    }
}