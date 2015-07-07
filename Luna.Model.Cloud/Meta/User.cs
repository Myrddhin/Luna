using System;

namespace Luna.Model.Cloud.Meta
{
    public class User : Entity
    {
        public Guid SubscriptionId { get; set; }

        public string AzureLogin { get; set; }

        public DateTime? LastQuery { get; set; }
    }
}