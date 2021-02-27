using System.Collections.Generic;
using seventh.Models;

namespace seventh.Resources
{
    public class ListServerResponse : BaseResponse
    {
        public ICollection<Server> Server { get; set; }

        public ListServerResponse(bool success, string message, ICollection<Server> server) : base(success, message)
        {
            Server = server;
        }

        public ListServerResponse(ICollection<Server> server) : this(true, string.Empty, server) { }

        public ListServerResponse(string message) : this(false, message, null) { }
    }
}