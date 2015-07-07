using System;
using Loki.Commands;
using Loki.Common;
using Loki.IoC;

namespace Luna.UI
{
    public static class LunaCommands
    {
        private static class Names
        {
            public const string RepositoryOpen = "Application.Open";
            public const string Configuration = "Application.Configuration";
            public const string Add = "Application.Add";
        }

        static LunaCommands()
        {
            if (!Toolkit.UI.Windows.DesignMode)
            {
                CreateCommands(Toolkit.IoC.DefaultContext);
            }
            else
            {
                CreateCommands(null);
            }
        }

        public static void CreateCommands(IObjectContext context)
        {
            Func<string, ICommand> builder = s => new LokiRelayCommand(() => { });
            if (context != null)
            {
                var commands = context.Get<ICommandComponent>();
                builder = name => commands.CreateCommand(name);
            }

            RepositoryOpen = builder(Names.RepositoryOpen);
            Configuration = builder(Names.Configuration);
            Add = builder(Names.Add);
        }

        public static ICommand RepositoryOpen { get; private set; }

        public static ICommand Configuration { get; private set; }

        public static ICommand Add { get; private set; }
    }
}