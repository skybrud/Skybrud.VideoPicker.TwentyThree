using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Json.Extensions;
using Skybrud.VideoPicker.Models.Videos;
using System.Collections.Generic;

namespace Skybrud.VideoPicker.TwentyThree.Provider
{
    public class TwentyThreeVideoDetails : IVideoDetails
    {
        #region Properties

        [JsonProperty("id")]
        public string Id { get; }

        [JsonProperty("channelId")]
        public string ChannelId { get; }

        [JsonProperty("title")]
        public string Title { get; }

        [JsonProperty("thumbnails")]
        public VideoThumbnail[] Thumbnails { get; }

        /// <summary>
        /// Gets the HTML embed code as received from the TwentyThree OEmbed endpoint.
        /// </summary>
        [JsonProperty("embed")]
        public string Embed { get; }

        #endregion

        #region Constructors

        public TwentyThreeVideoDetails(string videoId, string channelId, TwentyThreeOEmbed oembed)
        {
            Id = videoId;
            ChannelId = channelId;
            Title = oembed.Title;
            Thumbnails = new List<VideoThumbnail> {
                new VideoThumbnail(oembed.ThumbnailWidth, oembed.ThumbnailHeight, oembed.ThumbnailUrl),
            }.ToArray();
            Embed = oembed.Html;
        }

        public TwentyThreeVideoDetails(JObject obj)
        {
            Id = obj.GetString("id");
            ChannelId = obj.GetString("channelId");
            Title = obj.GetString("title");
            Thumbnails = obj.GetArrayItems("thumbnails", VideoThumbnail.Parse);
            Embed = obj.GetString("embed");
        }

        public static TwentyThreeVideoDetails Parse(JObject obj)
        {
            return obj == null ? null : new TwentyThreeVideoDetails(obj);
        }

        #endregion        
    }
}