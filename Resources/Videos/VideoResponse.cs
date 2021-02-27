using seventh.Models;

namespace seventh.Resources
{
    public class VideoResponse : BaseResponse
    {
        public Videos Video { get; set; }

        public VideoResponse(bool success, string message, Videos video) : base(success, message)
        {
            Video = video;
        }

        public VideoResponse(Videos video) : this(true, string.Empty, video) { }

        public VideoResponse(string message) : this(false, message, null) { }
    }
}