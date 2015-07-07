namespace Luna.Model.Storage
{
    public class RepositoryBase : Entity
    {
        public string Name { get; set; }

        public bool Online { get; set; }

        public decimal SchemaVersion { get; set; }
    }
}