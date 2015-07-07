using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using DevExpress.Xpf.Editors.Flyout;

namespace Luna.UI.CRM
{
    /// <summary>
    /// Interaction logic for ContactEditView.xaml
    /// </summary>
    public partial class ContactEditView
    {
        public ContactEditView()
        {
            InitializeComponent();

            editors.Add(titleEdit);
            editors.Add(phonesEdit);
            editors.Add(addressEdit);
        }

        private HashSet<FlyoutControl> editors = new HashSet<FlyoutControl>();

        private void StartEdit(object sender, MouseButtonEventArgs e)
        {
            var openEdit = editors.FirstOrDefault(x => x.IsOpen);
            var selectedEdit = editors.FirstOrDefault(x => x.PlacementTarget == sender);
            if (openEdit != null)
            {
                openEdit.IsOpen = false;
                e.Handled = true;
            }
            else if (selectedEdit != null)
            {
                EventHandler deleg = null;
                deleg = (s, ev) =>
                {
                    var parentElement = selectedEdit.PlacementTarget as FrameworkElement;
                    if (parentElement != null)
                    {
                        selectedEdit.Width = parentElement.Width;
                    }
                    selectedEdit.Opened -= deleg;
                };

                selectedEdit.Opened += deleg;
                selectedEdit.IsOpen = true;
                e.Handled = true;
            }
        }
    }
}