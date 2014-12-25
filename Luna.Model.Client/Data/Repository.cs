using System;

namespace Luna.Model.Storage
{
    public class Repository
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public decimal SchemaVersion { get; set; }

        public bool IsDefault { get; set; }

        public bool IsOnline { get; set; }

        public string ApiKey { get; set; }

        public string Path { get; set; }

        public Repository()
        {
            Id = new Guid();
        }
    }
}