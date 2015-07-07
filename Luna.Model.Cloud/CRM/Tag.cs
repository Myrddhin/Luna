using System;

namespace Luna.Model.Cloud.CRM
{
    public class Tag : ClientEntity
    {
        public Guid RepositoryId { get; set; }

        public string Name { get; set; }

        public string Color { get; set; }
    }
}