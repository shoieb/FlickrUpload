using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlickrNet;

namespace FlickrUpload
{
    class FlickrManager
    {
        public const string ApiKey = "f3854611c538269a2e9da81f41f37025";
        public const string SharedSecret = "628a82c6750942ae";

        public static Flickr GetInstance()
        {
            return new Flickr(ApiKey, SharedSecret);
        }

        public static Flickr GetAuthInstance()
        {
            var f = new Flickr(ApiKey, SharedSecret);
            f.OAuthAccessToken = OAuthToken.Token;
            f.OAuthAccessTokenSecret = OAuthToken.TokenSecret;
            return f;
        }

        public static OAuthAccessToken OAuthToken
        {
            get
            {
                return Properties.Settings.Default.OAuthToken;
            }
            set
            {
                Properties.Settings.Default.OAuthToken = value;
                Properties.Settings.Default.Save();
            }
        }
    }
}
