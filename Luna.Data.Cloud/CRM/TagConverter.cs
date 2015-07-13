using Luna.Model.CRM;

using CloudModel = Luna.Model.Cloud.CRM;

namespace Luna.Data.Cloud.CRM
{
    public class TagConverter : CloudConverter<Tag, CloudModel.Tag>
    {
        public override Tag FromCloud(CloudModel.Tag cloudItem)
        {
            var local = new Tag();
            local.LastUpdate = cloudItem.LastUpdate;
            local.Version = cloudItem.Version;
            local.Id = cloudItem.Id;
            local.Name = cloudItem.Name;
            local.Color = cloudItem.Color;
            local.RepositoryId = cloudItem.RepositoryId;

            return local;
        }

        public override CloudModel.Tag ToCloud(Tag localItem)
        {
            var remote = new CloudModel.Tag();
            remote.Name = localItem.Name;
            remote.Color = localItem.Color;
            remote.RepositoryId = localItem.RepositoryId;
            remote.Id = localItem.Id;
            remote.LastUpdate = localItem.LastUpdate;
            remote.Version = localItem.Version;

            return remote;
        }
    }
}