using System.Threading.Tasks;
using seventh.Models;
using seventh.Resources;

namespace seventh.Services
{
    public interface IVideosServices
    {
        Task<CreateVideoResponse> AddVideo(string serverId, CreateVideoResource video);
        Task<VideoResponse> GetVideo(string serverId, string videoId);
        Task<VideoResponse> RemoveVideo(string serverId, string videoId);
    }
}