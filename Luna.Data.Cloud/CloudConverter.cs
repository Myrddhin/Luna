namespace Luna.Data.Cloud
{
    public abstract class CloudConverter<TClient, TCloud>
    {
        public abstract TClient FromCloud(TCloud cloudItem);

        public abstract TCloud ToCloud(TClient localItem);
    }
}