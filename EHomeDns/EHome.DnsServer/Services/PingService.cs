using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EHome.DnsServer.Services
{
    public class PingService : IPingService
    {
        private string _clientIp;

        public void Set(string ipAddress)
        {
            _clientIp = ipAddress + "|" + DateTime.Now;
        }

        public string Get()
        {
            return _clientIp;
        }
    }
}