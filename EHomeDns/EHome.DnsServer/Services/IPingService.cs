using EHome.DnsServer.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace EHome.DnsServer.Services
{
    public interface IPingService
    {
        Task<Domain> GetAsync(string domainName);
        Task<Domain> SetAsync(string domainName, string ipAddress);
        Task<IEnumerable<Domain>> ListAsync();
    }
}