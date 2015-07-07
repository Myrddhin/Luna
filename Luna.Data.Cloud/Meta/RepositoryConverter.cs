using Luna.Model.Storage;
using Meta = Luna.Model.Cloud.Meta;

namespace Luna.Data.Cloud
{
    public class RepositoryConverter : CloudConverter<Repository, Meta.Repository>
    {
        public override Repository FromCloud(Meta.Repository cloudItem)
        {
            var local = new Repository();
            local.LastUpdate = cloudItem.LastUpdate;
            local.Version = cloudItem.Version;
            local.Id = cloudItem.Id;
            local.Name = cloudItem.Name;
            local.Online = true;
            local.SchemaVersion = cloudItem.SchemaVersion;

            return local;
        }

        public override Meta.Repository ToCloud(Repository localItem)
        {
            var remote = new Meta.Repository();
            remote.Name = localItem.Name;
            remote.SchemaVersion = localItem.SchemaVersion;
            remote.Parameters = localItem.Parameters;
            remote.Id = localItem.Id;
            remote.LastUpdate = localItem.LastUpdate;
            remote.Version = localItem.Version;

            return remote;
        }
    }
}