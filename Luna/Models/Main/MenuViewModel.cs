using Loki.Commands;
using Loki.UI;

namespace Luna.UI.Main
{
    public class MenuViewModel : ContainerAllActive<NavigationElement>
    {
        public MenuViewModel()
        {
            DisplayName = "Menu principal";
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();

            Items.Add(new CommandElement() { DisplayName = "Rechercher", Command = ApplicationCommands.Search });
            // Items.Add(new CommandElement() { DisplayName = "Tableau de bord", Command = new LokiRelayCommand(NavigateToDashboard) { Name = "Luna.Dashboard" } });
            //  Items.Add(new CommandElement() { DisplayName = "Geolocalisation", Command = new LokiRelayCommand(NavigateToGeolocalize) { Name = "Luna.Geolocalize" } });
            Items.Add(new CommandElement() { DisplayName = "Rafraîchir", Command = ApplicationCommands.Refresh });
            Items.Add(new CommandElement() { DisplayName = "Ajouter", Command = LunaCommands.Add });
            Items.Add(new CommandElement() { DisplayName = "Sauvegarder", Command = ApplicationCommands.Save });
        }
    }
}