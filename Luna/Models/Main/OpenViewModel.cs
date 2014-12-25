using System;
using System.ComponentModel;
using System.Linq;
using Loki.Common;
using Loki.UI;
using Luna.Model.Storage;
using Luna.Services.Configuration;

namespace Luna.UI.Main
{
    public class OpenViewModel : Screen
    {
        public OpenViewModel()
        {
            Existing = new BindableCollection<Repository>();
            New = new BindableCollection<Repository>();

            NewWeb = new Repository() { Id = Guid.NewGuid(), Name = "Base de donnée connectée", IsOnline = true };
            NewLocal = new Repository() { Id = Guid.NewGuid(), Name = "Base de donnée locale" };
        }

        public IApplicationConfiguration Application { get; set; }

        public BindableCollection<Repository> Existing { get; private set; }

        public BindableCollection<Repository> New { get; private set; }

        public Repository NewWeb { get; private set; }

        public Repository NewLocal { get; private set; }

        #region SelectedItem

        private PropertyChangedEventArgs selectedItemChangedEventArgs = ObservableHelper.CreateChangedArgs<OpenViewModel>(x => x.SelectedItem);

        private Repository selectedItem;

        public Repository SelectedItem
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

        protected override void OnInitialize()
        {
            base.OnInitialize();
            this.WatchPropertyChanged(this, v => v.SelectedItem, v => v.SelectedItem_Changed);
        }

        protected override void OnLoad()
        {
            ExistingRepositoryVisible = Application.Repositories.Any();
            Existing.Clear();
            Existing.AddRange(Application.Repositories);

            New.Clear();
            New.Add(NewLocal);
            New.Add(NewWeb);
        }

        private void SelectedItem_Changed(object sender, PropertyChangedEventArgs e)
        {
            this.TryClose(true);
        }
    }
}