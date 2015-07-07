using System.Collections.Generic;

namespace Luna.Services.Configuration
{
    public interface IRepositoryParameters
    {
        string CRMContactName { get; set; }

        string CRMAccountName { get; set; }

        string CRMSiteName { get; set; }

        string CRMSponsorName { get; set; }

        string ParameterSet { get; set; }

        void InitializeParameters(Dictionary<string, string> parameterSet);

        void UpgradeParameters();
    }
}