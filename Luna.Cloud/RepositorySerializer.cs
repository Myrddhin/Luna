using Client = Luna.Model.Storage;

namespace Luna.Web
{
    public static class RepositorySerializer
    {
        public static Client.Repository Convert(Web.Models.Repository source)
        {
            var result = new Client.Repository();
            result.Id = source.Id;
            result.Parameters = source.Parameters;
            result.IsDefault = source.IsDefault;
            result.IsOnline = source.IsOnline;
            result.Name = source.Name;
            result.LastUpdate = source.LastUpdate;
            result.SchemaVersion = source.SchemaVersion;

            return result;
        }
    }
}