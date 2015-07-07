using System;
using System.ComponentModel;
using Loki.Commands;
using Loki.Common;
using Loki.UI;
using Luna.Services.Configuration;

namespace Luna.UI.Main
{
    public class ConfigurationViewModel : Screen
    {
        public ConfigurationViewModel()
        {
            DisplayName = "Configuration";
        }

        public ICommand Validate { get; private set; }

        public IRepositoryConfigurationService Configuration { get; set; }

        #region ParameterSet

        private PropertyChangedEventArgs parameterSetChangedEventArgs = ObservableHelper.CreateChangedArgs<ConfigurationViewModel>(x => x.ParameterSet);

        private string parameterSet;

        public string ParameterSet
        {
            get
            {
                return parameterSet;
            }

            set
            {
                if (!object.Equals(parameterSet, value))
                {
                    parameterSet = value;
                    NotifyChanged(parameterSetChangedEventArgs);
                }
            }
        }

        #endregion ParameterSet

        protected override void OnInitialize()
        {
            Validate = Commands.Create();

            base.OnInitialize();

            Commands.Handle(Validate, Validate_CanExecute, Validate_Execute);
        }

        protected override void OnLoad()
        {
            base.OnLoad();
        }

        private void ParameterSet_Changed(object sender, EventArgs e)
        {
            switch (ParameterSet)
            {
                //case ParametersSets.SpectacleName:
                //    Configuration.Parameters.InitializeParameters(ParametersSets.Spectacle);
                //    this.Refresh();
                //    break;

                default:
                    break;
            }
        }

        private void Validate_CanExecute(object sender, CanExecuteCommandEventArgs e)
        {
            e.CanExecute = true;
        }

        private void Validate_Execute(object sender, CommandEventArgs e)
        {
            this.TryClose(true);
        }
    }
}