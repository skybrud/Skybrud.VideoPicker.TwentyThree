using Skybrud.VideoPicker.Models.Options;

namespace Skybrud.VideoPicker.TwentyThree.Provider
{
    public class TwentyThreeVideoOptions : IVideoOptions
    {
        public string ChannelId { get; }
        public string VideoId { get; }

        public TwentyThreeVideoOptions(string channelId, string videoId)
        {
            ChannelId = channelId;
            VideoId = videoId;
        }
    }
}