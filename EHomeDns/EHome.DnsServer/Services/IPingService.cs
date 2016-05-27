using EHome.DnsServer.Models;
using System.Threading.Tasks;

namespace EHome.DnsServer.Services
{
    public interface IPingService
    {
        Task<Domain> Get(string domainName);
        Task<string> Set(string domainName, string ipAddress);
    }
}