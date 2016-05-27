using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EHome.DnsServer.Services;
using Nancy;
using System.Threading.Tasks;

namespace EHome.DnsServer.Modules
{
    public class HomeModule : NancyModule
    {
        private readonly IPingService _pingService;

        public HomeModule(IPingService pingService)
        {
            _pingService = pingService;

            Get["/"] = _ =>
            {
                return "Hello from xedap629 DNS";
            };

            Get["/domain/{domain}/update/{ipAddresses}", true] = async (ctx, ct) =>
            {
                var domainName = ctx.domain;
                var clientIp = Request.UserHostAddress + "," + ctx.ipAddresses;
                var result = await _pingService.SetAsync(domainName, clientIp);
                return result;
            };

            Get["/domain/{domain}/ip", true] = async (ctx, ct) =>
            {
                return await _pingService.GetAsync((string)ctx.domain);
            };

            Get["/list", true] = async (ctx, ct) =>
            {
                return await _pingService.ListAsync();
            };
        }

    }
}