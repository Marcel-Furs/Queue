namespace Kolejki.API.Services
{
    public interface IMQTTService
    {
        void SendEmail(string receiver, string subject, string body);
    }
}
