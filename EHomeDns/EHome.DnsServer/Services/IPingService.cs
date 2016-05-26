namespace EHome.DnsServer.Services
{
    public interface IPingService
    {
        string Get();
        void Set(string ipAddress);
    }
}