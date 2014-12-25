using Loki.Common;
using Loki.UI;
using Luna.Services.Configuration;

namespace Luna.UI.Main
{
    public class MainViewModel : ContainerAllActive<Screen>
    {
        public IApplicationConfiguration Application { get; set; }

        public IRepositoryConfiguration RepositoryConfiguration { get; set; }

        public MainViewModel()
        {
            DisplayName = "Luna";
        }

        protected override void OnLoad()
        {
            base.OnLoad();
            if (RepositoryConfiguration.Current == null)
            {
                var openView = Context.Get<OpenViewModel>();

                Toolkit.UI.Windows.ShowAsPopup(openView);

                RepositoryConfiguration.Current = openView.SelectedItem;

                switch (RepositoryConfiguration.Status)
                {
                    case Luna.Model.Storage.RepositoryStatus.NeedUpgrade:
                        RepositoryConfiguration.Upgrade();
                        break;

                    case Luna.Model.Storage.RepositoryStatus.New:
                        RepositoryConfiguration.Create();
                        break;

                    case Luna.Model.Storage.RepositoryStatus.OK:
                    default:
                        break;
                }
            }
        }
    }
}