using Loki.UI;
using Luna.UI.CRM;

namespace Luna.UI.Main
{
    public class NavigationMenuViewModel : ContainerAllActive<NavigationElement>
    {
        public NavigationMenuViewModel()
        {
            DisplayName = "Navigation";
        }

        protected override void OnInitialize()
        {
            /* var geoSearchMessage = new NavigationMessage<GeoSearchViewModel>();
             var crmEntityListMessage = new NavigationMessage<EntityListViewModel>();

             var openFileMessage = new NavigationMessage<Screen>();
             var saveAsMessage = new NavigationMessage<Screen>();

             base.OnInitialize();
             var crmGroup = new NavigationGroupElement() { DisplayName = "CRM" };
             crmGroup.Children.Add(new MessageElement() { DisplayName = "Prospects", Message = crmEntityListMessage });
             crmGroup.Children.Add(new MessageElement() { DisplayName = "Geolocalization", Message = geoSearchMessage });
             Items.Add(crmGroup);

             var fileGroup = new NavigationGroupElement() { DisplayName = "Fichier" };
             fileGroup.Children.Add(new CommandElement() { DisplayName = "Ouvrir", Command = LunaCommands.FileOpen });
             fileGroup.Children.Add(new CommandElement() { DisplayName = "Enregistrer sous", Command = LunaCommands.FileSaveAs });
             Items.Add(fileGroup);*/

            var parametersMessage = new NavigationMessage<ConfigurationViewModel>();
            var importMessage = new DocumentMessage<ImportViewModel>();
            var configurationGroup = new NavigationGroupElement() { DisplayName = "Configuration" };
            configurationGroup.Children.Add(new MessageElement() { DisplayName = "Paramètres", Message = parametersMessage });
            configurationGroup.Children.Add(new MessageElement() { DisplayName = "Import", Message = importMessage });
            Items.Add(configurationGroup);

            var globalDirectory = new DocumentMessage<DirectoryViewModel>();
            var tags = new DocumentMessage<TagEditViewModel>();
            var crmGroup = new NavigationGroupElement() { DisplayName = "CRM" };
            crmGroup.Children.Add(new MessageElement() { DisplayName = "Annuaire", Message = globalDirectory });
            crmGroup.Children.Add(new MessageElement() { DisplayName = "Tags", Message = tags });
            Items.Add(crmGroup);
        }
    }
}