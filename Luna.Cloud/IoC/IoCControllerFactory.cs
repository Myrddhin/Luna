using System;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using Loki.IoC;

namespace Luna.Cloud.IoC
{
    public class IoCControllerActivator : IHttpControllerActivator
    {
        private readonly IObjectContext container;

        public IoCControllerActivator(IObjectContext container)
        {
            this.container = container;
        }

        public IHttpController Create(
            HttpRequestMessage request,
            HttpControllerDescriptor controllerDescriptor,
            Type controllerType)
        {
            var controller = (IHttpController)this.container.Get(controllerType);

            if (controller != null)
            {
                request.RegisterForDispose(
                    new Release(
                        () => this.container.Release(controller)));
            }

            return controller;
        }

        private class Release : IDisposable
        {
            private readonly Action release;

            public Release(Action release)
            {
                this.release = release;
            }

            public void Dispose()
            {
                this.release();
            }
        }
    }
}