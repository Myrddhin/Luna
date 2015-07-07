namespace Luna.Model.IO
{
    public interface IDataConverterFactory
    {
        IDataConverterService GetStandardExcel();

        void Release(IDataConverterService service);
    }
}