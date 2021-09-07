using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Json.Extensions;

namespace Skybrud.VideoPicker.TwentyThree.Provider
{
    public class TwentyThreeOEmbed
    {
        public string Title { get; internal set; }
        public int ThumbnailWidth { get; internal set; }
        public int ThumbnailHeight { get; internal set; }
        public string ThumbnailUrl { get; internal set; }
        public string Html { get; internal set; }

        public TwentyThreeOEmbed(JObject obj)
        {
            Title = obj.GetString("title");
            Html = obj.GetString("html");
            ThumbnailUrl = obj.GetString("thumbnail_url");
            ThumbnailWidth = obj.GetInt32("thumbnail_width");
            ThumbnailHeight = obj.GetInt32("thumbnail_height");
        }
    }
}