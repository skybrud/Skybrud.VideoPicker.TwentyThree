using Skybrud.Essentials.Xml.Extensions;
using Skybrud.VideoPicker.Models.Config;
using System.Xml.Linq;

namespace Skybrud.VideoPicker.TwentyThree.Provider
{
    public class TwentyThreeConfig : IProviderConfig
    {
        public TwentyThreeCredentials[] Credentials { get; }

        public TwentyThreeConfig(XElement xml)
        {
            Credentials = xml.GetElements("credentials", x => new TwentyThreeCredentials(x));
        }
    }
}