using System.Net;
using System.Threading.Tasks;

namespace AeroGear
{
    public interface HttpClient
    {
        Task<HttpStatusCode> Send(UnifiedMessage message);
        void setUsernamePassword(string username, string password);
    }
}
