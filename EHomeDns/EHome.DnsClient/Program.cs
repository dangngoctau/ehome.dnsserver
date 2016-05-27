using System;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace EHome.DnsClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var localIpAddresses = GetLocalIpAddresses();
            var dnsServerAddress = ConfigurationManager.AppSettings["DnsServerAddress"];
            var clientDomain = ConfigurationManager.AppSettings["ClientDomain"];
            var interval = Convert.ToInt32(ConfigurationManager.AppSettings["Interval"]);
            while (true)
            {
                Task t = Ping(dnsServerAddress, clientDomain, localIpAddresses);
                t.Wait();
                Thread.Sleep(TimeSpan.FromMinutes(interval));
            }
        }

        static async Task Ping(string dnsServer, string domain, string[] localIpAddresses)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri(dnsServer);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = await client.GetAsync(string.Format("domain/{0}/update/{1}", domain, string.Join(",", localIpAddresses)));
                    response.EnsureSuccessStatusCode();
                    Domain result = await response.Content.ReadAsAsync<Domain>();
                    Console.WriteLine("Your address: {0}", result.Address);
                }
                catch (HttpRequestException ex)
                {
                    Console.WriteLine("Error on sending Ping Request to DNS Server {0}:\n {1}\n", domain, ex.ToString());
                }
            }
        }

        static string[] GetLocalIpAddresses()
        {
            return Dns.GetHostEntry(Dns.GetHostName()).AddressList
                .Where(c => c.AddressFamily == AddressFamily.InterNetwork)
                .Select(c => c.ToString()).OrderByDescending(c=>c).ToArray();
        }
    }

    public class Domain
    {
        public int DomainId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
