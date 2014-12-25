using System;
using System.IO;
using Loki.UI;

namespace Luna.Services.Configuration
{
    internal class BaseConfigurationService : TrackedObject
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
    }
}