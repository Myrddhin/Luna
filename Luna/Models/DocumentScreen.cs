using System;
using Loki.Common;
using Loki.UI;

namespace Luna.UI
{
    public class DocumentScreen : AsyncScreen
    {
        public override void CanClose(Action<bool> callback)
        {
            bool canClose = true;
            if (IsChanged)
            {
                canClose = Toolkit.UI.Windows.Confirm("Certains changements n'ont pas été sauvegardés ; les annuler ?");
            }

            callback(canClose);
        }
    }
}