using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Json.Extensions;
using Skybrud.VideoPicker.PropertyEditors;

namespace Skybrud.VideoPicker.TwentyThree.Provider
{
    public class TwentyThreeDataTypeConfig : IProviderDataTypeConfig
    {
        [JsonProperty("enabled")]
        public bool IsEnabled { get; }

        [JsonProperty("autoplay")]
        public DataTypeConfigOption<bool> Autoplay { get; }

        public TwentyThreeDataTypeConfig()
        {
            IsEnabled = false;
            Autoplay = new DataTypeConfigOption<bool>(false);
        }

        public TwentyThreeDataTypeConfig(JObject value)
        {
            IsEnabled = value.GetBoolean("enabled");
            Autoplay = new DataTypeConfigOption<bool>(value.GetBoolean("autoplay.value"));
        }
    }
}