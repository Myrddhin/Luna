using System;

namespace Luna.Model.Cloud.Meta
{
    public class Subscription : Entity
    {
        public string Name { get; set; }

        public DateTime Start { get; set; }

        public DateTime? Closing { get; set; }
    }
}