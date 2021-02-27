using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using seventh.Data;
using seventh.Models;
using seventh.Resources;

namespace seventh.Services
{
    public class VideosServices : IVideosServices
    {
        private readonly IMapper _mapper;
        private DataContext _dataContext;
        private IServerServices _serverServices;

        public VideosServices(IMapper mapper, DataContext dataContext, IServerServices serverServices)
        {
            _mapper = mapper;
            _dataContext = dataContext;
            _serverServices = serverServices;
        }

        public async Task<CreateVideoResponse> AddVideo(string serverId, CreateVideoResource video)
        {
            try
            {
                var server = await _serverServices.FindServerById(serverId);
                if (!server.Success)
                    return new CreateVideoResponse("Server not found");

                if (await FindVideo(video, serverId) == true)
                    return new CreateVideoResponse("Video já inserido no servidor");

                var encoded = Regex.Replace(video.VideoBase64, @"data:video\/.{3,7};base64,", String.Empty);
                encoded = encoded.Replace("data:application/pdf;base64,", String.Empty);
                var type = Regex.Replace(video.VideoBase64, @";.*", String.Empty);
                var contentType = type.Replace("data:", "");
                // var type = Regex.Replace(image, @";.*", String.Empty);
                byte[] ret = Convert.FromBase64String(encoded);
                var model = _mapper.Map<CreateVideoResource, Videos>(video);
                model.ServerId = server.Server.Id;
                model.sizeInBytes = ret.Length;
                model.Id = Guid.NewGuid().ToString();

                await _dataContext.Videos.AddAsync(model);
                await _dataContext.SaveChangesAsync();

                return new CreateVideoResponse(model);

            }
            catch (Exception ex)
            {
                return new CreateVideoResponse(ex.Message);
            }
        }

        public async Task<bool> FindVideo(CreateVideoResource video, string serverId)
        {
            try
            {
                var result = await _dataContext.Videos.AsNoTracking().FirstOrDefaultAsync(x => x.VideoBase64 == video.VideoBase64 && x.ServerId == serverId);
                if (result == null)
                    return false;

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<VideoResponse> GetVideo(string serverId, string videoId)
        {
            try
            {
                var video = await _dataContext.Videos.AsNoTracking().FirstOrDefaultAsync(x => x.ServerId == serverId && x.Id == videoId);
                if (video == null)
                    return new VideoResponse("Video not found");
                return new VideoResponse(video);
            }
            catch (Exception ex)
            {
                return new VideoResponse(ex.Message);
            }
        }

        public async Task<VideoResponse> RemoveVideo(string serverId, string videoId)
        {
            try
            {
                var result = await GetVideo(serverId, videoId);
                if (!result.Success)
                    return new VideoResponse("Video not found");


                _dataContext.Remove(result.Video);
                await _dataContext.SaveChangesAsync();

                return new VideoResponse(result.Video);

            }
            catch (Exception ex)
            {
                return new VideoResponse(ex.Message);
            }
        }


    }
}
