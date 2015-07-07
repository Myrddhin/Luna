using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Luna.Data.Local.Storage;
using Luna.Model.Storage;
using Luna.Services.Configuration;

namespace Luna.Data.Storage
{
    internal class ApplicationDataProvider : BaseProvider, IApplicationDataProvider
    {
        public IRepositoryStore RepositoryStore { get; set; }

        public IApplicationSettingsDataContext DataContext
        {
            get;
            set;
        }

        public async Task<IEnumerable<Repository>> GetCloudRepositoriesAsync()
        {
            return await RepositoryStore.GetAllAsync();
        }

        public IQueryable<RepositorySettings> LocalRepositories
        {
            get { return DataContext.RepositorySettings; }
        }

        public async Task SaveAsync(RepositorySettings repository)
        {
            await Save<RepositorySettings>(DataContext, DataContext.RepositorySettings, repository);
        }

        public async Task SaveChangesAsync()
        {
            //foreach (var repository in DataContext.ChangeTracker.Entries<Repository>())
            //{
            //    if (repository.State != EntityState.Unchanged && repository.Entity.Id == ClientDataContainer.InternalRepositoryId)
            //    {
            //        ConfigurationContext.Repositories.Attach(repository.Entity);
            //        ConfigurationContext.SetState(repository.Entity, repository.State);
            //    }
            //}

            await DataContext.SaveChangesAsync();
            //await ConfigurationContext.SaveChangesAsync();
        }

        /* #region Context (application and client) synchronisation

         public override void BeginInit()
         {
             base.BeginInit();

             if (ApplicationDataContainer.ConnectionString != null)
             {
                 DataContext = ApplicationDataContainer.GetApplicationSettingsDataContext();
             }

             this.ApplicationDataContainer.ConnectionChanged += ApplicationStore_ConnectionChanged;
         }

         protected override void CleanClientContextes()
         {
             ConfigurationContext.SafeDispose();
         }

         protected override void GetClientContextes()
         {
             ConfigurationContext = ClientDataContainer.GetConfigurationContext();
         }

         private void ApplicationStore_ConnectionChanged(object sender, System.EventArgs e)
         {
             DataContext.SafeDispose();

             if (ApplicationDataContainer.ConnectionString != null)
             {
                 DataContext = ApplicationDataContainer.GetApplicationSettingsDataContext();
             }
         }

         #endregion Context (application and client) synchronisation*/
    }
}