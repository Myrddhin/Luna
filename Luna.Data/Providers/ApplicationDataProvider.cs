using System;
using System.ComponentModel;
using System.Linq;
using Loki.Common;
using Luna.Data.Local.Storage;
using Luna.Model.Storage;

namespace Luna.Data.Storage
{
    internal class ApplicationDataProvider : BaseObject, IApplicationDataProvider
    {
        private Lazy<IApplicationDataContext> globalContext;

        public ApplicationDataProvider()
        {
            globalContext = new Lazy<IApplicationDataContext>(() => ApplicationStore.GetApplicationDataContext());
        }

        public IQueryable<Repository> Repositories
        {
            get { return globalContext.Value.Repositories; }
        }

        protected IApplicationDataContext DataContext
        {
            get { return globalContext.Value; }
        }

        public IGlobalRepositoryManager ApplicationStore
        {
            get;
            set;
        }

        IRepositoryManager IApplicationDataProvider.ApplicationStore
        {
            get { return this.ApplicationStore; }
        }

        #region Property changed

        /// <summary>
        /// Se produit lorsqu'une valeur de propriété est modifiée.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises the <see cref="PropertyChanged" /> event.
        /// </summary>
        /// <param name="e"><see cref="PropertyChangedEventArgs" /> object that provides the
        /// arguments for the event.</param>
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)
            {
                handler(this, e);
            }
        }

        #endregion Property changed

        #region Current

        private Repository current;

        private PropertyChangedEventArgs currentChangedArgs = ObservableHelper.CreateChangedArgs<ApplicationDataProvider>(x => x.Current);

        public Repository Current
        {
            get
            {
                return current;
            }

            set
            {
                if (value != current)
                {
                    OnPropertyChanged(currentChangedArgs);
                    current = value;
                }
            }
        }

        #endregion Current

        IRepositoryManager IApplicationDataProvider.ClientStore
        {
            get { return this.ClientStore; }
        }

        public ILocalRepositoryManager ClientStore
        {
            get;
            set;
        }

        public void SaveRepository(Repository repository)
        {
            var old = DataContext.Repositories.Find(repository.Id);
            if (old == null)
            {
                DataContext.Repositories.Add(repository);
            }
            else
            {
                DataContext.Repositories.Attach(repository);
                DataContext.MarkAsModified(repository);
            }

            globalContext.Value.SaveChanges();
        }
    }
}