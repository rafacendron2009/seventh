using seventh.Models;

namespace seventh.Resources
{
    public class ServerModelResponse : BaseResponse
    {
        public Server Server { get; set; }

        public ServerModelResponse(bool success, string message, Server server) : base(success, message)
        {
            Server = server;
        }

        public ServerModelResponse(Server server) : this(true, string.Empty, server) { }

        public ServerModelResponse(string message) : this(false, message, null) { }
    }
}