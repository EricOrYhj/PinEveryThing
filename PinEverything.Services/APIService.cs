using PinEverything.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PinEverything.Services
{
    public class APIService
    {
        private AuthModel GetAccessToken(
           string appKey,
           string appSecret,
           string code,
           string redirectUri)
        {
            AuthModel result = new AuthModel();
            string apiResult = new MySpider.MyHttpInterFace().PostWebRequest(
                            string.Format("app_key={0}&app_secret={1}&grant_type=authorization_code&format=json&code={2}&redirect_uri={3}",
                                appKey,
                                appSecret,
                                code,
                                redirectUri
                            ),
                            MySpider.ConfigHelper.GetConfigString("ApiAddress") + "/oauth2/access_token",
                            Encoding.UTF8
                        );
            return result;
        }
    }
}
