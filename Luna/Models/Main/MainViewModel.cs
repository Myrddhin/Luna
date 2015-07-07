using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Loki.Common;
using Loki.UI;
using Luna.Services.Configuration;

namespace Luna.UI.Main
{
    public class MainViewModel : ContainerAllActive<Screen>, IHandle<NavigationMessage<ConfigurationViewModel>>
    {
        public NavigationMenuViewModel Navigation
        {
            get { return Items.OfType<NavigationMenuViewModel>().FirstOrDefault(); }
            set { EnsureItem(value); }
        }

        public DocumentsViewModel Documents
        {
            get { return Items.OfType<DocumentsViewModel>().FirstOrDefault(); }
            set { EnsureItem(value); }
        }

        public MenuViewModel Menu
        {
            get { return Items.OfType<MenuViewModel>().FirstOrDefault(); }
            set { EnsureItem(value); }
        }

        public IScreenFactory ScreenFactory { get; set; }

        public IRepositoryManagerService RepositoryManager { get; set; }

        #region RepositoryName

        private PropertyChangedEventArgs repositoryNameChangedEventArgs = ObservableHelper.CreateChangedArgs<MainViewModel>(x => x.RepositoryName);

        private string repositoryName;

        public string RepositoryName
        {
            get
            {
                return repositoryName;
            }

            set
            {
                if (!object.Equals(repositoryName, value))
                {
                    repositoryName = value;
                    NotifyChanged(repositoryNameChangedEventArgs);
                }
            }
        }

        #endregion RepositoryName

        #region UserName

        private PropertyChangedEventArgs argsuserNameChanged = ObservableHelper.CreateChangedArgs<MainViewModel>(x => x.UserName);

        private string userName;

        public string UserName
        {
            get
            {
                return userName;
            }

            set
            {
                if (!object.Equals(userName, value))
                {
                    userName = value;
                    NotifyChanged(argsuserNameChanged);
                }
            }
        }

        #endregion UserName

        public MainViewModel()
        {
            DisplayName = "Luna";
        }

        protected override void OnLoad()
        {
            base.OnLoad();

            this.Activated += OpenScreen;
        }

        private void OpenScreen(object sender, EventArgs e)
        {
            this.Activated -= OpenScreen;

            bool needConfiguration = false;
            ScopedScreen<OpenViewModel>(openViewModel =>
            {
                var defaultRepository = RepositoryManager.LocalRepositories.FirstOrDefault(x => x.Default);
                if (defaultRepository == null)
                {
                    Windows.ShowAsPopup(openViewModel);

                    needConfiguration |= openViewModel.SelectedItem == openViewModel.NewWeb
                     || openViewModel.SelectedItem == openViewModel.NewLocal;

                    var selectedItem = openViewModel.OnlineMode ? openViewModel.SelectedOnlineItem : openViewModel.SelectedItem;
                    RepositoryManager.ConnectAsync(selectedItem);
                }
                else
                {
                    RepositoryManager.ConnectAsync(defaultRepository);
                }

                Task.WaitAll(Task.Run(async () =>
                {
                    switch (RepositoryManager.Status)
                    {
                        case Luna.Model.Storage.RepositoryStatus.NeedUpgrade:
                            await RepositoryManager.UpgradeAsync();
                            break;

                        case Luna.Model.Storage.RepositoryStatus.New:
                            await RepositoryManager.CreateAsync();
                            break;

                        case Luna.Model.Storage.RepositoryStatus.OK:
                        default:
                            break;
                    }
                }));

                needConfiguration |= !RepositoryManager.IsReady();
            });

            ScopedScreen<ConfigurationViewModel>(configurationViewModel =>
            {
                RepositoryName = configurationViewModel.Configuration.Name;

                bool? result = null;
                if (needConfiguration)
                {
                    result = Toolkit.UI.Windows.ShowAsPopup(configurationViewModel);
                }

                if (result ?? false)
                {
                    Task.WaitAll(configurationViewModel.Configuration.SaveChangesAsync());
                    RepositoryName = configurationViewModel.Configuration.Name;

                    UserName = RepositoryManager.UserName;
                }
            });
        }

        public void Handle(NavigationMessage<ConfigurationViewModel> message)
        {
            bool? result = null;

            ScopedScreen<ConfigurationViewModel>(configurationViewModel =>
            {
                result = Toolkit.UI.Windows.ShowAsPopup(configurationViewModel);

                if (result ?? false)
                {
                    Task.WaitAll(RepositoryManager.SaveChangesAsync());
                    RepositoryName = configurationViewModel.Configuration.Name;
                }
            });
        }

        private void ScopedScreen<T>(Action<T> handler) where T : Screen
        {
            T screen = null;

            try
            {
                screen = ScreenFactory.Create<T>();
                handler(screen);
            }
            finally
            {
                ScreenFactory.Release(screen);
            }
        }
    }
}