using seventh.Models;

namespace seventh.Resources
{
    public class ServerResponse : BaseResponse
    {
        public ServerResource Server { get; set; }

        public ServerResponse(bool success, string message, ServerResource server) : base(success, message)
        {
            Server = server;
        }

        public ServerResponse(ServerResource server) : this(true, string.Empty, server) { }

        public ServerResponse(string message) : this(false, message, null) { }
    }
}