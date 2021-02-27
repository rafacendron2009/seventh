using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using seventh.Resources;
using seventh.Services;

namespace seventh.Controllers.v1
{

    [ApiController]
    public class VideosControllers : ControllerBase
    {
        private IVideosServices _videosServices;

        public VideosControllers(IVideosServices videosServices)
        {
            _videosServices = videosServices;
        }

        [HttpPost]
        [EnableCors]
        [Route("api/servers/{serverId}/videos")]
        public async Task<ActionResult<dynamic>> AddVideo(string serverId, [FromBody] CreateVideoResource video)
        {
            var response = await _videosServices.AddVideo(serverId, video);
            if (!response.Success)
            {
                return BadRequest(new ProblemDetails()
                {
                    Type = "https://httpstatuses.com/400",
                    Title = ReasonPhrases.GetReasonPhrase(400),
                    Status = 400,
                    Detail = response.Message
                });
            }
            return StatusCode(201, response.Videos);
        }

        [HttpGet]
        [EnableCors]
        [Route("api/servers/{serverId}/videos/{videoId}")]
        public async Task<ActionResult<dynamic>> GetVideo(string serverId, string videoId)
        {
            var response = await _videosServices.GetVideo(serverId, videoId);
            if (!response.Success)
            {
                return BadRequest(new ProblemDetails()
                {
                    Type = "https://httpstatuses.com/400",
                    Title = ReasonPhrases.GetReasonPhrase(400),
                    Status = 400,
                    Detail = response.Message
                });
            }
            return StatusCode(201, response.Video);
        }

        [HttpDelete]
        [EnableCors]
        [Route("api/servers/{serverId}/videos/{videoId}")]
        public async Task<ActionResult<dynamic>> RemoveVideo(string serverId, string videoId)
        {
            var response = await _videosServices.RemoveVideo(serverId, videoId);
            if (!response.Success)
            {
                return BadRequest(new ProblemDetails()
                {
                    Type = "https://httpstatuses.com/400",
                    Title = ReasonPhrases.GetReasonPhrase(400),
                    Status = 400,
                    Detail = response.Message
                });
            }
            return Ok();
        }


        [HttpGet]
        [EnableCors]
        [Route("api/servers/{serverId}/videos/{videoId}/binary")]
        public async Task<ActionResult<dynamic>> Testea(string serverId, string videoId)
        {
            var response = await _videosServices.GetVideo(serverId, videoId);
            if (!response.Success)
            {
                return BadRequest(new ProblemDetails()
                {
                    Type = "https://httpstatuses.com/400",
                    Title = ReasonPhrases.GetReasonPhrase(400),
                    Status = 400,
                    Detail = response.Message
                });
            }
            var encoded = Regex.Replace(response.Video.VideoBase64, @"data:video\/.{3,7};base64,", String.Empty);
            encoded = encoded.Replace("data:application/pdf;base64,", String.Empty);
            var type = Regex.Replace(response.Video.VideoBase64, @";.*", String.Empty);
            var contentType = type.Replace("data:", "");
            byte[] ret = Convert.FromBase64String(encoded);
            return File(ret, contentType);
        }
    }
}