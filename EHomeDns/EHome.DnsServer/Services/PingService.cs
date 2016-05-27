using Dapper;
using EHome.DnsServer.Models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace EHome.DnsServer.Services
{
    public class PingService : IPingService
    {
        public async Task<Domain> SetAsync(string domainName, string ipAddress)
        {
            using (var connection = GetConnectionString())
            {
                connection.Open();

                var domain = (await connection.QueryAsync<Domain>("select * from domains where Name = @Name", new { Name = domainName })).SingleOrDefault();
                if (domain == null)
                {
                    domain = new Domain
                    {
                        Name = domainName,
                        Address = ipAddress,
                        LastUpdated = DateTime.UtcNow
                    };

                    await connection.ExecuteAsync("insert into domains(Name, Address, LastUpdated) values(@Name, @Address, @LastUpdated)", domain);
                }
                else if (domain.Address != ipAddress)
                {
                    domain.Address = ipAddress;
                    domain.LastUpdated = DateTime.UtcNow;
                    await connection.ExecuteAsync("update domains set Address = @Address, LastUpdated = @LastUpdated where DomainId = @DomainId ", domain);
                }

                return domain;
            }
        }

        public async Task<Domain> GetAsync(string domainName)
        {
            using (var connection = GetConnectionString())
            {
                connection.Open();
                var result = await connection.QueryAsync<Domain>("select * from domains where Name = @Name", new { Name = domainName });
                return result.SingleOrDefault();
            }
        }

        public async Task<IEnumerable<Domain>> ListAsync()
        {
            using (var connection = GetConnectionString())
            {
                connection.Open();
                var result = await connection.QueryAsync<Domain>("select * from domains");
                return result;
            }
        }

        private SQLiteConnection GetConnectionString()
        {
            return new SQLiteConnection(string.Format("Data Source={0}AppData\\dns.db", System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath));
        }
    }
}