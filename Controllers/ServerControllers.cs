using System;
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
    public class ServerControllers : ControllerBase
    {
        private IConfiguration _configuration;
        private IServerServices _serverServices;

        public ServerControllers(IConfiguration configuration, IServerServices serverServices)
        {
            _configuration = configuration;
            _serverServices = serverServices;
        }

        [HttpPost]
        [EnableCors]
        [Route("api/server")]
        public async Task<ActionResult<dynamic>> CreateServer([FromBody] CreateServerResource server)
        {
            var response = await _serverServices.CreateServer(server);
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
            return StatusCode(201, response.Server);
        }

        [HttpDelete]
        [EnableCors]
        [Route("api/server/{serverId}")]
        public async Task<ActionResult<dynamic>> RemoveServer(string serverId)
        {
            var response = await _serverServices.RemoveServer(serverId);
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
        [Route("api/server")]
        public async Task<ActionResult<dynamic>> ListServer()
        {
            var response = await _serverServices.ListServer();
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
            return Ok(response.Server);
        }


        [HttpGet]
        [EnableCors]
        [Route("api/servers/available/{serverId}")]
        public async Task<ActionResult<dynamic>> FindServerById(string serverId)
        {
            var response = await _serverServices.FindServerById(serverId);
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
        [Route("api/servers/{serverId}/videos")]
        public async Task<ActionResult<dynamic>> GetVideoByServer(string serverId)
        {
            var response = await _serverServices.ListVideosByserver(serverId);
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
            return Ok(response.Server);
        }
    }
}