using System;
using System.Collections.Generic;

namespace Luna.Services.Configuration
{
    public class ParametersSets
    {
        private static Lazy<Dictionary<string, string>> spectacle = new Lazy<Dictionary<string, string>>(CreateSpectacle);

        public static Dictionary<string, string> Spectacle
        {
            get
            {
                return spectacle.Value;
            }
        }

        public const string SpectacleName = "Spectacle";

        private static Dictionary<string, string> CreateSpectacle()
        {
            var buffer = new Dictionary<string, string>();
            buffer.Add(ParametersNames.ParameterSet, SpectacleName);
            buffer.Add(ParametersNames.CRMContactName, "Contact");
            buffer.Add(ParametersNames.CRMSiteName, "Lieu");
            buffer.Add(ParametersNames.CRMSponsorName, "Sponsor");
            buffer.Add(ParametersNames.CRMAccountName, "Structure");
            return buffer;
        }
    }
}