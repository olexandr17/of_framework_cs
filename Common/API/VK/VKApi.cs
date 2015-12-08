using System;
using System.Collections.Specialized;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;

namespace OFClassLibrary.Common.API.VK
{
    class VKApi
    {

        private const string AUTH_URL = "https://oauth.vk.com/";
        private const string API_URL = "https://api.vk.com/";

        public event EventHandler<VKApiEventArgs> NeedAuthorize;
        public event EventHandler<VKApiEventArgs> Authorized;
        public event EventHandler<VKApiEventArgs> GotProfiles;
        public event EventHandler<VKApiEventArgs> GotFriends;
        public event EventHandler<VKApiEventArgs> GotAudio;
        public event EventHandler<VKApiEventArgs> GotAlbums;
        public event EventHandler<VKApiEventArgs> GotPhotos;


        public readonly WebBrowser Browser;
        public string UserId { get; private set; }

        private bool needAuthorize = false;
        private bool needUserInfo = false;

        private int appID;
        private int scope;

        private string accessToken;    // Ключ
        private int expiresIn;         // Продолжительность жизни ключа в секундах

        public VKApi(int appID, int scope)
        {
            Browser = new WebBrowser();
            Browser.ScriptErrorsSuppressed = true;
            Browser.DocumentCompleted += browserCompletedHandler;

            this.appID = appID;
            this.scope = scope;
        }

        public void Start()
        {
            NameValueCollection _values = new NameValueCollection();
            _values["client_id"] = appID.ToString();
            _values["scope"] = scope.ToString();
            _values["redirect_uri"] = AUTH_URL + "blank.html";
            _values["display"] = "popup";
            _values["response_type"] = "token";

            var vars = joinValues(_values);
            Browser.Navigate($"{AUTH_URL}authorize?{vars}");
        }

        public void GetProfiles(string userID = null)
        {
            var values = new NameValueCollection()
            {
                ["uid"] = (userID ?? UserId).ToString(),
                ["fields"] = "uid,first_name,last_name,nickname,screen_name,sex,bdate,city,country,timezone,photo,photo_medium,photo_big,has_mobile,rate,contacts,education,online,counters"
            };

            if (needUserInfo)
            {
                needUserInfo = false;
                executeCommand("users.get", values, Authorized);
            }
            else
            {
                executeCommand("users.get", values, GotProfiles);
            }
        }

        public void GetFriends(string userID = null)
        {
            var values = new NameValueCollection()
            {
                ["uid"] = (userID ?? UserId).ToString(),
                ["fields"] = "uid,first_name,last_name,nickname,sex,bdate,city,country,timezone,photo,photo_medium,photo_big,domain,has_mobile,rate,contacts,education",
                ["order"] = "hints"
            };

            executeCommand("friends.get", values, GotFriends);
        }

        public void GetAudio(string userID = null)
        {
            var values = new NameValueCollection()
            {
                ["uid"] = (userID ?? UserId).ToString()
            };

            executeCommand("audio.get", values, GotAudio);
        }

        public void GetAlbums(string userID = null)
        {
            var values = new NameValueCollection()
            {
                ["uid"] = (userID ?? UserId).ToString(),
                ["need_covers"] = "1"
            };

            executeCommand("photos.getAlbums", values, GotAlbums);
        }

        public void GetPhotos(string userID, string albumID)
        {
            var values = new NameValueCollection()
            {
                ["uid"] = userID.ToString(),
                ["aid"] = albumID.ToString()
            };

            executeCommand("photos.get", values, GotPhotos);
        }


        private void browserCompletedHandler(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            //var urlParams = HttpUtility.ParseQueryString(e.Url.Fragment.Substring(1));
            //accessToken = urlParams.Get("access_token");
            //UserId = urlParams.Get("user_id");

            var url = e.Url.ToString();
            if (url.IndexOf("access_token") != -1)
            {
                var reg = new Regex(@"(?<name>[\w\d\x5f]+)=(?<value>[^\x26\s]+)", RegexOptions.IgnoreCase | RegexOptions.Singleline);
                var matches = reg.Matches(url);
                foreach (Match m in matches)
                {
                    var group = m.Groups;
                    var name = group["name"].Value;
                    var value = group["value"].Value;

                    switch (name)
                    {
                        case "user_id":
                            UserId = value;
                            break;
                        case "access_token":
                            accessToken = value;
                            break;
                        case "expires_in":
                            expiresIn = Convert.ToInt32(value);
                            break;
                    }
                }

                needUserInfo = true;

                GetProfiles(UserId);
            }
            else
            {
                if (!needAuthorize)
                {
                    needAuthorize = true;
                    NeedAuthorize?.Invoke(this, new VKApiEventArgs());
                }
            }
        }

        private void executeCommand(string name, NameValueCollection values, EventHandler<VKApiEventArgs> handler)
        {
            if (values["access_token"] == null)
                values["access_token"] = accessToken;

            var vars = joinValues(values);

            var doc = new XmlDocument();
            doc.Load($"{API_URL}method/{name}.xml?{vars}");

            handler?.Invoke(this, new VKApiEventArgs(doc));
        }

        private string joinValues(NameValueCollection values) => string.Join("&", values.AllKeys.Select(i => i + "=" + values[i]));

    }
}
