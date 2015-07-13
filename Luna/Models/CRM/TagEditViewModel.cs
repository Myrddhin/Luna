using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Loki.Commands;
using Loki.Common;
using Loki.UI;
using Loki.UI.Tasks;

using Luna.Data.CRM;
using Luna.Model.CRM;
using Luna.Model.Storage;

namespace Luna.UI.CRM
{
    public class TagEditViewModel : DocumentScreen
    {
        public ICRMDataProvider DataProvider { get; set; }

        public IEnumerable<CommandElement> RowCommands
        {
            get
            {
                return new CommandElement[] { new CommandElement() { Command = TestCommand, DisplayName = "TestCommand" } };
            }
        }

        public ICommand TestCommand { get; private set; }

        public TagEditViewModel()
        {
            DisplayName = "Tags";
            Tags = new BindableCollection<Tag>();
        }

        private ITaskConfiguration<object, IEnumerable<Tag>> cloudGetter;

        public BindableCollection<Tag> Tags { get; private set; }

        protected override void OnInitialize()
        {
            TestCommand = Commands.Create();

            base.OnInitialize();

            Commands.Handle(LunaCommands.Add, Command_Add_Execute);
            Commands.Handle(TestCommand, TestCommand_CanExecute, TestCommand_Execute);

            cloudGetter = CreateWorker<object, IEnumerable<Tag>>("Chargement", CloudGetter_DoWork);
        }

        protected async override void OnLoad()
        {
            var tags = await cloudGetter.DoWorkAsync(null);

            Tags.Clear();
            Tags.AddRange(tags);

            base.OnLoad();
        }

        private async Task<IEnumerable<Tag>> CloudGetter_DoWork(object o)
        {
            await DataProvider.EnsureCloudRefresh();
            await Task.Delay(5000);
            return DataProvider.Tags;
        }

        private void Command_Add_Execute(object sender, CommandEventArgs e)
        {
            ApplicationCommands.Open.Execute(new Tag());
        }

        private void TestCommand_Execute(object sender, CommandEventArgs e)
        {
            var items = e.Parameter as IEnumerable;
            Toolkit.UI.Signals.Message(string.Join(";", items.OfType<Tag>().Select(x => x.Name)));
        }

        private void TestCommand_CanExecute(object sender, CanExecuteCommandEventArgs e)
        {
            e.CanExecute |= e.Parameter != null && ((IEnumerable)e.Parameter).OfType<Tag>().Count() > 1;
        }
    }
}