using System;
using Loki.Common;
using Loki.UI;

namespace Luna.Services.Common
{
    internal class BaseService : TrackedObject, IDisposable
    {
        ~BaseService()
        {
            Dispose(false);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
        }

        public void Dispose()
        {
            Dispose(true);
        }

        public IMessageComponent MessageBus { get; set; }
    }
}