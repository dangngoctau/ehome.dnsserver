using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EHome.DnsServer.Services;
using Nancy;
using System.Threading.Tasks;

namespace EHome.DnsServer.Modules
{
    public class HomeModule: NancyModule
    {
        private readonly IPingService _pingService;

        public HomeModule(IPingService pingService)
        {
            _pingService = pingService;

            Get["/"] = _ =>
            {
                return "Hello";
            };

            Get["/domain/{domain}/update", true] = async (ctx, ct)=>
            {
                var domainName = ctx.domain;
                var clientIp = Request.UserHostAddress;
                var result = await _pingService.Set((string)domainName, (string)clientIp);
                return result;
            };
            
            Get["/domain/{domain}/ip", true] = async (ctx, ct) =>
            {
                return await Task.FromResult(System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath); //_pingService.Get((string)ctx.domain);
            };
        }

    }
}