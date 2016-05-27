using System;

namespace EHome.DnsServer.Models
{
    public class Domain
    {
        public int DomainId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}