using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EHome.DnsServer.Services;
using Nancy;

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
                _pingService.Set(Request.UserHostAddress);
                return "Hello";
            };

            Get["/nic/update"] = _ =>
            {
                _pingService.Set(Request.UserHostAddress);
                return "Hello update";
            };
            
            Get["/ip"] = _ =>
            {          
                return _pingService.Get();
            };
        }

    }
}