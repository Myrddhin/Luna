using System.Linq;
using Loki.Commands;
using Loki.UI;
using Luna.Model.CRM;
using Luna.UI.CRM;

namespace Luna.UI.Main
{
    public class DocumentsViewModel : ContainerOneActive<DocumentScreen>, IDocumentContainer
    {
        public IScreenFactory ScreenFactory { get; set; }

        public DocumentsViewModel()
        {
            this.Items.CollectionChanged += Items_CollectionChanged;
        }

        private void Items_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems != null)
            {
                foreach (var item in e.OldItems.OfType<Screen>())
                {
                    ScreenFactory.Release(item);
                }
            }
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();

            Commands.Handle(ApplicationCommands.Open, Open_Contact_CanExecute, Open_Contact_Execute);
        }

        private void Open_Contact_CanExecute(object sender, CanExecuteCommandEventArgs e)
        {
            e.CanExecute |= e.Parameter is Contact;
        }

        private void Open_Contact_Execute(object sender, CommandEventArgs e)
        {
            var contact = e.Parameter as Contact;
            if (contact != null)
            {
                ContactEditViewModel item = null;

                foreach (var candidate in Children.OfType<ContactEditViewModel>())
                {
                    if (candidate.Current != null && candidate.Current.Id == contact.Id)
                    {
                        item = candidate;
                    }
                }

                if (item == null)
                {
                    item = ScreenFactory.Create<ContactEditViewModel>();
                    EnsureItem(item);
                    item.Current = contact;
                }

                ActivateItem(item);
            }
        }

        private void Handle<T>(INavigationMessage message) where T : DocumentScreen
        {
            DocumentScreen item = null;

            foreach (var candidate in Children)
            {
                if (message.Match(candidate))
                {
                    item = candidate;
                }
            }

            if (item == null)
            {
                item = ScreenFactory.Create<T>() as DocumentScreen;
                EnsureItem(item);
                message.Initialize(item);
            }

            ActivateItem(item);
        }

        public void Handle(NavigationMessage<TagEditViewModel> message)
        {
            Handle<TagEditViewModel>(message);
        }

        public void Handle(NavigationMessage<DirectoryViewModel> message)
        {
            Handle<DirectoryViewModel>(message);
        }

        public void Handle(NavigationMessage<ContactEditViewModel> message)
        {
            Handle<ContactEditViewModel>(message);
        }

        public void Handle(NavigationMessage<ImportViewModel> message)
        {
            Handle<ImportViewModel>(message);
        }
    }
}