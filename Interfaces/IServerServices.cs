using System.Threading.Tasks;
using seventh.Resources;

namespace seventh.Services
{
    public interface IServerServices
    {
        Task<CreateServerResponse> CreateServer(CreateServerResource server);
        Task<ServerResponse> RemoveServer(string serverId);
        Task<ServerResponse> FindServerById(string serverId);
        Task<ListServerResponse> ListServer();
        Task<ServerModelResponse> ListVideosByserver(string serverId);
    }
}