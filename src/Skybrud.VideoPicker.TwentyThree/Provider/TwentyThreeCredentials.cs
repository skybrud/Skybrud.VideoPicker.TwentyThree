using Skybrud.Essentials.Xml.Extensions;
using Skybrud.VideoPicker.Models.Config;
using System;
using System.Xml.Linq;

namespace Skybrud.VideoPicker.TwentyThree.Provider
{
    public class TwentyThreeCredentials : IProviderCredentials
    {
        public Guid Id { get; }

        public string Name { get; }

        public string AccessToken { get; }

        public string AccessTokenSecret { get; }

        public string ConsumerKey { get; }

        public string ConsumerSecret { get; }

        public bool IsConfigured { get; }

        public TwentyThreeCredentials(XElement xml)
        {
            Id = xml.GetAttributeValue("id", Guid.Parse);
            Name = xml.GetAttributeValue("name");
            AccessToken = xml.GetAttributeValue("accessToken");
            AccessTokenSecret = xml.GetAttributeValue("accessTokenSecret");
            ConsumerKey = xml.GetAttributeValue("consumerKey");
            ConsumerSecret = xml.GetAttributeValue("consumerSecret");
            IsConfigured = string.IsNullOrWhiteSpace(AccessToken) == false || string.IsNullOrWhiteSpace(ConsumerKey) == false && string.IsNullOrWhiteSpace(ConsumerSecret) == false;
        }
    }
}