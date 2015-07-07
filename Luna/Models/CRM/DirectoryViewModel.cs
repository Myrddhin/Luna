using System.Threading.Tasks;
using Loki.Commands;
using Loki.UI;
using Luna.Data.CRM;
using Luna.Model.CRM;

namespace Luna.UI.CRM
{
    public class DirectoryViewModel : DocumentScreen
    {
        public ICRMDataProvider DataProvider { get; set; }

        public DirectoryViewModel()
        {
            DisplayName = "Annuaire";
            Contacts = new BindableCollection<Contact>();
        }

        public BindableCollection<Contact> Contacts { get; private set; }

        protected override void OnInitialize()
        {
            base.OnInitialize();

            Commands.Handle(LunaCommands.Add, Command_Add_Execute);
        }

        protected override void OnLoad()
        {
            Contacts.Clear();
            Contacts.AddRange(DataProvider.Contacts);

            Task.Delay(5000);

            base.OnLoad();
        }

        private void Command_Add_Execute(object sender, CommandEventArgs e)
        {
            ApplicationCommands.Open.Execute(new Contact());
        }
    }
}