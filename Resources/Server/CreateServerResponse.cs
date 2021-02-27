using seventh.Models;

namespace seventh.Resources
{
    public class CreateServerResponse : BaseResponse
    {
        public ServerResource Server { get; set; }

        public CreateServerResponse(bool success, string message, ServerResource server) : base(success, message)
        {
            Server = server;
        }

        public CreateServerResponse(ServerResource server) : this(true, string.Empty, server) { }

        public CreateServerResponse(string message) : this(false, message, null) { }
    }
}