using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Loki.Commands;
using Loki.Common;
using Loki.UI;
using Loki.UI.Tasks;
using Luna.Model.Storage;
using Luna.Services.Configuration;

namespace Luna.UI.Main
{
    public class OpenViewModel : AsyncScreen
    {
        public OpenViewModel()
        {
            Local = new BindableCollection<RepositorySettings>();
            Online = new BindableCollection<RepositoryBase>();
            DisplayName = "Ouverture";

            NewWeb = new RepositoryBase() { Id = Guid.NewGuid(), Name = "Base de donnée connectée", Online = true };
            NewLocal = new RepositoryBase() { Id = Guid.NewGuid(), Name = "Base de donnée locale", Online = false };
        }

        private ITaskConfiguration<object, IEnumerable<RepositoryBase>> cloudGetter;

        public IRepositoryManagerService RepositoryManager { get; set; }

        public BindableCollection<RepositorySettings> Local { get; private set; }

        public BindableCollection<RepositoryBase> Online { get; private set; }

        public RepositoryBase NewWeb { get; private set; }

        public RepositoryBase NewLocal { get; private set; }

        public ICommand New { get; private set; }

        #region SelectedItem

        private PropertyChangedEventArgs selectedItemChangedEventArgs = ObservableHelper.CreateChangedArgs<OpenViewModel>(x => x.SelectedItem);

        private RepositoryBase selectedItem;

        public RepositoryBase SelectedItem
        {
            get
            {
                return selectedItem;
            }

            set
            {
                if (!Object.Equals(selectedItem, value))
                {
                    selectedItem = value;
                    NotifyChanged(selectedItemChangedEventArgs);
                }
            }
        }

        #endregion SelectedItem

        #region SelectedOnlineItem

        private PropertyChangedEventArgs argsselectedOnlineItemChanged = ObservableHelper.CreateChangedArgs<OpenViewModel>(x => x.SelectedOnlineItem);

        private RepositoryBase selectedOnlineItem;

        public RepositoryBase SelectedOnlineItem
        {
            get
            {
                return selectedOnlineItem;
            }

            set
            {
                if (!object.Equals(selectedOnlineItem, value))
                {
                    selectedOnlineItem = value;
                    NotifyChanged(argsselectedOnlineItemChanged);
                }
            }
        }

        #endregion SelectedOnlineItem

        #region ExistingRepositoryVisible

        private static PropertyChangedEventArgs existingRepositoryVisibleChanged = ObservableHelper.CreateChangedArgs<OpenViewModel>(x => x.ExistingRepositoryVisible);

        private bool existingRepositoryVisible = false;

        public bool ExistingRepositoryVisible
        {
            get
            {
                return existingRepositoryVisible;
            }

            set
            {
                if (value != existingRepositoryVisible)
                {
                    existingRepositoryVisible = value;
                    NotifyChanged(existingRepositoryVisibleChanged);
                }
            }
        }

        #endregion ExistingRepositoryVisible

        #region OnlineMode

        private PropertyChangedEventArgs argsonlineModeChanged = ObservableHelper.CreateChangedArgs<OpenViewModel>(x => x.OnlineMode);

        private bool onlineMode;

        public bool OnlineMode
        {
            get
            {
                return onlineMode;
            }

            set
            {
                if (!object.Equals(onlineMode, value))
                {
                    onlineMode = value;
                    NotifyChanged(argsonlineModeChanged);
                }
            }
        }

        #endregion OnlineMode

        protected override void OnInitialize()
        {
            New = Commands.Create();
            base.OnInitialize();
            this.WatchPropertyChanged(this, v => v.SelectedItem, v => v.SelectedItem_Changed);
            this.WatchPropertyChanged(this, v => v.SelectedOnlineItem, v => v.SelectedOnlineItem_Changed);

            cloudGetter = CreateWorker<object, IEnumerable<RepositoryBase>>("Chargement", CloudGetter_DoWork);

            Commands.Handle(New, New_Execute);
        }

        private void New_Execute(object sender, CommandEventArgs e)
        {
            SelectedItem = e.Parameter as RepositoryBase;
        }

        protected override void OnLoad()
        {
            Local.Clear();
            Local.AddRange(RepositoryManager.LocalRepositories);
            ExistingRepositoryVisible = Local.Any();
        }

        private async Task<IEnumerable<RepositoryBase>> CloudGetter_DoWork(object o)
        {
            return await RepositoryManager.GetCloudRepositoriesAsync();
        }

        private async void SelectedItem_Changed(object sender, PropertyChangedEventArgs e)
        {
            if (SelectedItem == NewWeb)
            {
                var repos = await cloudGetter.DoWorkAsync(null);
                if (repos != null && repos.Any())
                {
                    Online.Clear();
                    Online.AddRange(repos);
                    ExistingRepositoryVisible = false;
                    OnlineMode = true;
                }
                else
                {
                    Information("Aucune base de donnée disponible.");
                }
            }
            else
            {
                this.TryClose(true);
            }
        }

        private void SelectedOnlineItem_Changed(object sender, PropertyChangedEventArgs e)
        {
            this.TryClose(true);
        }
    }
}