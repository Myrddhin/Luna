namespace Luna.UI.Main
{
    /// <summary>
    /// Interaction logic for ConfigurationView.xaml
    /// </summary>
    public partial class ConfigurationView
    {
        public ConfigurationView()
        {
            InitializeComponent();
        }

        private void ListBoxEdit_SelectedIndexChanged(object sender, System.Windows.RoutedEventArgs e)
        {
            GRP_CRMParamsDetails.IsEnabled = LST_CRMParams.SelectedIndex == LST_CRMParams.Items.Count - 1;
        }
    }
}