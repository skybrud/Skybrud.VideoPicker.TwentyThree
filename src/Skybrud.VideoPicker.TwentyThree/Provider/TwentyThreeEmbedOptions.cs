using HtmlAgilityPack;
using Newtonsoft.Json;
using Skybrud.Essentials.Http.Collections;
using Skybrud.VideoPicker.Models.Videos;
using System.Web;

namespace Skybrud.VideoPicker.TwentyThree.Provider
{
    public class TwentyThreeEmbedOptions : IVideoEmbedOptions
    {
        private readonly TwentyThreeVideoDetails _details;

        #region Properties

        /// <summary>
        /// Indicates whether the video should automatically start to play when the player loads.
        /// </summary>
        [JsonProperty("autoplay")]
        public bool Autoplay { get; }

        #endregion

        #region Constructors

        public TwentyThreeEmbedOptions(TwentyThreeVideoDetails details) : this(details, null) { }

        public TwentyThreeEmbedOptions(TwentyThreeVideoDetails details, TwentyThreeDataTypeConfig config)
        {

            _details = details;

            config = config ?? new TwentyThreeDataTypeConfig();
            Autoplay = config.Autoplay.Value;

        }

        #endregion        

        #region Member methods

        public IHtmlString GetHtml()
        {
            return GetHtml(854, 480);
        }

        public IHtmlString GetHtml(int width, int height)
        {

            HttpQueryString query = new HttpQueryString();
            if (Autoplay) query.Add("autoplay", "1");

            HtmlDocument document = new HtmlDocument();

            HtmlNode iframe = document.CreateElement("iframe");

            iframe.Attributes.Add("frameborder", "0");
            iframe.Attributes.Add("width", width.ToString());
            iframe.Attributes.Add("height", height.ToString());

            if (Autoplay) iframe.Attributes.Add("allow", "autoplay");

            iframe.Attributes.Add("allowfullscreen", string.Empty);
            iframe.Attributes.Add("webkitallowfullscreen", string.Empty);
            iframe.Attributes.Add("mozallowfullscreen", string.Empty);

            if (string.IsNullOrWhiteSpace(_details.Title) == false) iframe.Attributes.Add("title", _details.Title);

            return new HtmlString(
                iframe.OuterHtml
                    .Replace("mozallowfullscreen=\"\"", "mozallowfullscreen")
                    .Replace("webkitallowfullscreen=\"\"", "webkitallowfullscreen")
                    .Replace("allowfullscreen=\"\"", "allowfullscreen")
            );

        }

        #endregion
    }
}