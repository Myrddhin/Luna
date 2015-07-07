using Loki.IoC;

namespace Luna.Cloud.Install
{
    public static class GlobalInstaller
    {
        private static readonly DataInstaller data = new DataInstaller();

        public static DataInstaller Data
        {
            get { return data; }
        }

        private static readonly ControllerInstaller controllers = new ControllerInstaller();

        public static ControllerInstaller Controller
        {
            get { return controllers; }
        }

        public static IContextInstaller All
        {
            get { return LokiContextInstaller.Merge(Data, Controller); }
        }
    }
}