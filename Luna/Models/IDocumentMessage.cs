using Loki.Common;
using Loki.UI;

namespace Luna.UI
{
    public interface IDocumentHandler<T> : IHandle<NavigationMessage<T>> where T : DocumentScreen
    {
    }
}