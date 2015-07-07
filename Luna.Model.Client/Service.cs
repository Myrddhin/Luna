using System;
using System.IO;
using Loki.UI;

namespace Luna.Model
{
    public class Service : TrackedObject, IDisposable
    {
        protected const decimal GlobalSchemaVersion = 1;

        protected const decimal RepositorySchemaVersion = 1;

        protected string DataFolder
        {
            get
            {
                return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData, Environment.SpecialFolderOption.Create), "Luna");
            }
        }

        ~Service()
        {
            Dispose(false);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
        }

        public void Dispose()
        {
            Dispose(true);
        }
    }
}