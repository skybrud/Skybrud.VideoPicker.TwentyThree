using Skybrud.Essentials.Xml.Extensions;
using Skybrud.Social.TwentyThree;
using Skybrud.Social.TwentyThree.OAuth;
using Skybrud.VideoPicker.Exceptions;
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

        public TwentyThreeService GetService()
        {            
            if (!string.IsNullOrWhiteSpace(AccessToken) 
                && !string.IsNullOrWhiteSpace(AccessTokenSecret) 
                && !string.IsNullOrWhiteSpace(ConsumerKey) 
                && !string.IsNullOrWhiteSpace(ConsumerSecret))
            {
                TwentyThreeOAuthClient client = new TwentyThreeOAuthClient(ConsumerKey, ConsumerSecret, AccessToken, AccessTokenSecret, "");
                return TwentyThreeService.CreateFromOAuthClient(client);
            }
               

            throw new VideosException("Vimeo credentials isn't configured.");

        }
    }
}
