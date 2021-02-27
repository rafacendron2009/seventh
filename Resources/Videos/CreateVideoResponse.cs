using seventh.Models;

namespace seventh.Resources
{
    public class CreateVideoResponse : BaseResponse
    {
        public Videos Videos { get; set; }

        public CreateVideoResponse(bool success, string message, Videos videos) : base(success, message)
        {
            Videos = videos;
        }

        public CreateVideoResponse(Videos videos) : this(true, string.Empty, videos) { }

        public CreateVideoResponse(string message) : this(false, message, null) { }
    }
}