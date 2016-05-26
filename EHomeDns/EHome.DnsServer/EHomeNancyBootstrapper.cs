using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EHome.DnsServer.Services;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.Responses;
using Nancy.TinyIoc;

namespace EHome.DnsServer
{
    public class EHomeNancyBootstrapper: DefaultNancyBootstrapper
    {
        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
            container.Register<IPingService, PingService>().AsSingleton();
            base.ConfigureApplicationContainer(container);
        }
    }


}