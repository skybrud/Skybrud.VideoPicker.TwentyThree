using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Http;
using Skybrud.Essentials.Http.Collections;
using Skybrud.Essentials.Json;
using Skybrud.Essentials.Json.Extensions;
using Skybrud.VideoPicker.Exceptions;
using Skybrud.VideoPicker.Models;
using Skybrud.VideoPicker.Models.Config;
using Skybrud.VideoPicker.Models.Options;
using Skybrud.VideoPicker.Models.Providers;
using Skybrud.VideoPicker.PropertyEditors;
using Skybrud.VideoPicker.Providers;
using Skybrud.VideoPicker.Services;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace Skybrud.VideoPicker.TwentyThree.Provider
{
    public class TwentyThreeVideoProvider : IVideoProvider
    {
        public string Alias => "twentythree";

        public string Name => "TwentyThree";

        public string ConfigView => "/App_Plugins/Skybrud.VideoPicker.TwentyThree/Views/Config.html";

        public string EmbedView => null;

        public bool IsMatch(VideoPickerService service, string source, out IVideoOptions options)
        {
            options = null;

            // Remove query string (if present)
            source = source.Split('?')[0];

            Match m1 = Regex.Match(source, "^(http|https)://([a-zA-Z0-9-\\.]+)/manage/video/([0-9]+)$", RegexOptions.IgnoreCase);

            // TODO: Change options to fit twentythree
            if (m1.Success)
            {
                options = new TwentyThreeVideoOptions(m1.Groups[2].Value, m1.Groups[3].Value);
            }

            // Get a reference to the TwentyThree provider configuration
            TwentyThreeConfig config = service.Config.GetConfig<TwentyThreeConfig>(this);

            // Get the first credentials (or trigger an error if none)
            TwentyThreeCredentials credentials = config?.Credentials.FirstOrDefault();
            if (credentials == null) throw new VideosException("TwentyThree provider is not configured (1).");
            if (credentials.IsConfigured == false) throw new VideosException("TwentyThree provider is not configured (2).");

            return options != null;
        }
        public VideoPickerValue GetVideo(VideoPickerService service, IVideoOptions options)
        {
            if (!(options is TwentyThreeVideoOptions o)) return null;

            string oembedUrl = "https://video.gubi.com/oembed?" + new HttpQueryString {
                {"url", $"https://video.gubi.com/manage/video/{o.VideoId}"},
                {"format", "json"}
            };

            // Get video information from the oembed endpoint
            IHttpResponse response = HttpUtils.Requests.Get(oembedUrl);
            if (response.StatusCode != HttpStatusCode.OK) return null;

            TwentyThreeOEmbed oembed = new TwentyThreeOEmbed(JsonUtils.ParseJsonObject(response.Body));

            VideoProviderDetails provider = new VideoProviderDetails(Alias, Name);

            TwentyThreeVideoDetails details = new TwentyThreeVideoDetails(o.VideoId, o.ChannelId, oembed);

            TwentyThreeEmbedOptions embed = new TwentyThreeEmbedOptions(details);

            return new VideoPickerValue(provider, details, embed);
        }

        public IProviderConfig ParseConfig(XElement xml)
        {
            return new TwentyThreeConfig(xml);
        }

        public IProviderDataTypeConfig ParseDataTypeConfig(JObject obj)
        {
            return new TwentyThreeDataTypeConfig(obj);
        }

        public VideoPickerValue ParseValue(JObject json, IProviderDataTypeConfig config)
        {
            VideoProviderDetails provider = new VideoProviderDetails(Alias, Name);

            TwentyThreeVideoDetails details = json.GetObject("details", TwentyThreeVideoDetails.Parse);

            TwentyThreeEmbedOptions embed = new TwentyThreeEmbedOptions(details, config as TwentyThreeDataTypeConfig);

            return new VideoPickerValue(provider, details, embed);
        }
    }
}
