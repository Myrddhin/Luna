using Luna.Model.Storage;
using CloudModel = Luna.Model.Cloud.Meta;

namespace Luna.Data.Cloud.Meta
{
    public class RepositoryConverter : CloudConverter<Repository, CloudModel.Repository>
    {
        public override Repository FromCloud(CloudModel.Repository cloudItem)
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

        public override CloudModel.Repository ToCloud(Repository localItem)
        {
            var remote = new CloudModel.Repository();
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