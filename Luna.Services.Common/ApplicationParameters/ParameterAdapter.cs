using System.Collections.Generic;
using System.Linq;
using Luna.Data.Storage;
using Luna.Model.Configuration;

namespace Luna.Services.Configuration
{
    internal class ParameterAdapter : IRepositoryParameters
    {
        private IApplicationDataProvider dataProvider;

        public ParameterAdapter(IApplicationDataProvider provider)
        {
            dataProvider = provider;
        }

        public void InitializeParameters(Dictionary<string, string> defaultParameters)
        {
            foreach (var config in defaultParameters)
            {
                var parameter = dataProvider.Parameters.FirstOrDefault(x => x.Name == config.Key);
                if (parameter == null)
                {
                    dataProvider.Add(new ApplicationParameter() { Name = config.Key, Value = config.Value });
                }
                else
                {
                    parameter.Value = config.Value;
                }
            }
        }

        public void UpgradeParameters()
        {
            var parameterSet = dataProvider.Parameters.First(x => x.Name == ParametersNames.ParameterSet);

            switch (parameterSet.Value)
            {
                case ParametersSets.SpectacleName:
                default:
                    UpgradeParameterSet(ParametersSets.Spectacle);
                    break;
            }
        }

        private void UpgradeParameterSet(Dictionary<string, string> defaultParameters)
        {
            foreach (var config in defaultParameters)
            {
                var parameter = dataProvider.Parameters.FirstOrDefault(x => x.Name == config.Key);
                if (parameter == null)
                {
                    dataProvider.Add(new ApplicationParameter() { Name = config.Key, Value = config.Value });
                }
            }
        }

        private string GetParameterValue(string name)
        {
            return dataProvider.Parameters.FirstOrDefault(x => x.Name == name).Value;
        }

        private void SetParameterValue(string name, string value)
        {
            dataProvider.Parameters.First(x => x.Name == name).Value = value;
        }

        public string CRMContactName
        {
            get
            {
                return GetParameterValue(ParametersNames.CRMContactName);
            }
            set
            {
                SetParameterValue(ParametersNames.CRMContactName, value);
            }
        }

        public string CRMAccountName
        {
            get
            {
                return GetParameterValue(ParametersNames.CRMAccountName);
            }
            set
            {
                SetParameterValue(ParametersNames.CRMAccountName, value);
            }
        }

        public string CRMSiteName
        {
            get
            {
                return GetParameterValue(ParametersNames.CRMSiteName);
            }
            set
            {
                SetParameterValue(ParametersNames.CRMSiteName, value);
            }
        }

        public string CRMSponsorName
        {
            get
            {
                return GetParameterValue(ParametersNames.CRMSponsorName);
            }
            set
            {
                SetParameterValue(ParametersNames.CRMSponsorName, value);
            }
        }

        public string ParameterSet
        {
            get
            {
                return GetParameterValue(ParametersNames.ParameterSet);
            }
            set
            {
                SetParameterValue(ParametersNames.ParameterSet, value);
            }
        }
    }
}