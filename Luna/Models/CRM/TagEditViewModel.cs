using System.ComponentModel;
using System.Threading.Tasks;
using Loki.Commands;
using Loki.Common;
using Luna.Data.CRM;
using Luna.Model.CRM;

namespace Luna.UI.CRM
{
    public class TagEditViewModel : DocumentScreen
    {
        public ICRMDataProvider DataContext { get; set; }

        public Tag Current { get; set; }

        #region Name

        private readonly PropertyChangedEventArgs ArgspropertyBackerChanged = ObservableHelper.CreateChangedArgs<TagEditViewModel>(x => x.Name);

        public string Name
        {
            get
            {
                return Current.Name;
            }

            set
            {
                if (!object.Equals(Current.Name, value))
                {
                    Current.Name = value;
                    NotifyChangedAndDirty(ArgspropertyBackerChanged);
                }
            }
        }

        #endregion Name

        #region Color

        private readonly PropertyChangedEventArgs ArgsColorChanged = ObservableHelper.CreateChangedArgs<TagEditViewModel>(x => x.Color);

        public string Color
        {
            get
            {
                return Current.Color;
            }

            set
            {
                if (!object.Equals(Current.Color, value))
                {
                    Current.Color = value;
                    NotifyChanged(ArgsColorChanged);
                }
            }
        }

        #endregion Color

        protected override void OnInitialize()
        {
            base.OnInitialize();

            Commands.Handle(ApplicationCommands.Save, Save_CanExecute, Save_Execute);
        }

        private void Save_Execute(object sender, CommandEventArgs e)
        {
            Task.WaitAll(DataContext.SaveAsync(Current));
            Task.WaitAll(DataContext.SaveChangesAsync());
        }

        private void Save_CanExecute(object sender, CanExecuteCommandEventArgs e)
        {
            e.CanExecute |= !string.IsNullOrWhiteSpace(Name) && !string.IsNullOrWhiteSpace(Color);
        }
    }
}