using PinEverything.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

using Newtonsoft.Json;


namespace PinEverything.Services
{
    public class APIService
    {
        public static AuthModel GetAccessToken(
           string appKey,
           string appSecret,
           string grant_type,
           string code,
           string redirectUri)
        {
            AuthModel result = new AuthModel();
            string apiResult = new MySpider.MyHttpInterFace().GetWebRequest(
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


        public static string GetAccessToken(string code)
        {
            string access_token = string.Empty;
            if (!string.IsNullOrEmpty(code))
            {
                string appKey = MySpider.ConfigHelper.GetConfigString("AppKey");
                string AppSecret = MySpider.ConfigHelper.GetConfigString("AppSecret");
                string redirectUri = MySpider.ConfigHelper.GetConfigString("CallbackUrl");
                string grant_type = "authorization_code";

                string Url = "http://api.mingdao.com/auth2/access_token";
                string QueryStr = "app_key=" + appKey + "&app_secret=" + AppSecret + "&grant_type=" + grant_type + "" +
                    "&code=" + code + "&format=json&redirect_uri=" + redirectUri + "";
                string requestURL = HttpGet(Url, QueryStr);

                JavaScriptObject accessObj = new JavaScriptObject();
                if (!string.IsNullOrEmpty(requestURL))
                    accessObj = (JavaScriptObject)JavaScriptConvert.DeserializeObject(requestURL);

                if (accessObj != null)
                    access_token = accessObj["access_token"].ToString();
            }
            return access_token;
        }

        /// <summary>
        /// 获取授权账号基本信息
        /// </summary>
        /// <param name="access_token"></param>
        /// <returns>用户对象</returns>
        public static JavaScriptObject getUserDetail(string access_token)
        {
            JavaScriptObject userDetailObj = new JavaScriptObject();

            string Url = "https://api.mingdao.com/passport/detail";//授权账号基本信息
            string detailStr = "access_token=" + access_token + "&format=json";

            string requestURL = HttpGet(Url, detailStr);
            JavaScriptObject resultObj = new JavaScriptObject();
            if (!string.IsNullOrEmpty(requestURL))
            {
                resultObj = (JavaScriptObject)JavaScriptConvert.DeserializeObject(requestURL);
                userDetailObj = (JavaScriptObject)resultObj["user"];
            }
            return userDetailObj;
        }

        /// <summary>
        /// 根据发布内容分享到明道
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="pMsg">动态内容</param>
        /// <param name="title">链接标题</param>
        /// <returns>是否成功</returns>
        public string postUpdate(string access_token, string pMsg, string title,string postUrl)
        {
            string postID = string.Empty;
            if (!string.IsNullOrEmpty(access_token))
            {
                string Url = "https://api.mingdao.com/post/update";//接口地址
           
                //参数
                Dictionary<string, string> paramsDic = new Dictionary<string, string>();
                paramsDic.Add("access_token", access_token);
                paramsDic.Add("format", "json");
                paramsDic.Add("p_type", "1");
                paramsDic.Add("p_msg", pMsg);
                paramsDic.Add("l_uri", postUrl);
                paramsDic.Add("l_title", title);
                paramsDic.Add("s_type", "3");

                string requestURL = HttpPost(Url, paramsDic);

                JavaScriptObject resultObj = new JavaScriptObject();
                if (!string.IsNullOrEmpty(requestURL))
                    resultObj = (JavaScriptObject)JavaScriptConvert.DeserializeObject(requestURL);

                if (!string.IsNullOrEmpty(resultObj["post"].ToString()))
                    postID = resultObj["post"].ToString();
            }
            return postID;
        }

        /// <summary>
        /// 发送私信
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="u_id">接收人ID</param>
        /// <param name="msg">私信内容</param>
        /// <param name="is_send_mobilephone">是否发送短信</param>
        /// <returns></returns>
        public bool sendMsg(string access_token, string u_id, string msg, string is_send_mobilephone,string p_id)
        {
            bool flag = false;
            if (!string.IsNullOrEmpty(access_token))
            {
                string Url = "https://api.mingdao.com/message/create";//接口地址

                //参数
                Dictionary<string, string> paramsDic = new Dictionary<string, string>();
                paramsDic.Add("access_token", access_token);
                paramsDic.Add("u_id", u_id);//接收人ID
                paramsDic.Add("msg", msg);//消息内容
                paramsDic.Add("is_send_mobilephone", is_send_mobilephone);//是否同时发送短信(0不发1发)
                paramsDic.Add("format", "json");

                string requestURL = HttpPost(Url, paramsDic);

                JavaScriptObject resultObj = new JavaScriptObject();
                if (!string.IsNullOrEmpty(requestURL))
                    resultObj = (JavaScriptObject)JavaScriptConvert.DeserializeObject(requestURL);

                if (!string.IsNullOrEmpty(resultObj["message"].ToString()))
                    flag = true;

                string postUrl = "https://api.mingdao.com/post/add_reply";//接口地址

                //参数
                Dictionary<string, string> postDic = new Dictionary<string, string>();
                postDic.Add("access_token", access_token);
                postDic.Add("p_id", p_id);//动态ID
                postDic.Add("r_msg", msg);//消息内容
                postDic.Add("format", "json");
                string postRequest = HttpPost(postUrl, postDic);
            }
            return flag;
        }

        public static string HttpPost(string url, Dictionary<string, string> paraDic)
        {
            try
            {
                string paraStr = string.Empty;
                if (paraDic != null && paraDic.Count > 0)
                {
                    foreach (string key in paraDic.Keys)
                    {
                        if (string.IsNullOrEmpty(paraStr))
                            paraStr = key + "=" + paraDic[key];
                        else
                            paraStr += "&" + key + "=" + paraDic[key];
                    }
                }

                byte[] bData = Encoding.UTF8.GetBytes(paraStr.ToString());

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "POST";
                request.Timeout = 2000;
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = bData.Length;

                System.IO.Stream smWrite = request.GetRequestStream();
                smWrite.Write(bData, 0, bData.Length);
                smWrite.Close();

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                System.IO.Stream dataStream = response.GetResponseStream();
                System.IO.StreamReader reader = new System.IO.StreamReader(dataStream, Encoding.UTF8);
                string responseFromServer = reader.ReadToEnd();
                reader.Close();
                dataStream.Close();
                response.Close();

                return responseFromServer;
            }
            catch { }

            return null;
        }


        public static string HttpGet(string Url, string QueryStr)
        {
            string ResponseData = null;
            if (!string.IsNullOrEmpty(QueryStr))
            {
                Url = Url + "?" + QueryStr;
            }
            HttpWebRequest WebReq = WebRequest.Create(Url) as HttpWebRequest;
            WebReq.Method = "GET";
            WebReq.ServicePoint.Expect100Continue = false;
            WebReq.Timeout = 20000;

            StreamReader Reader = null;
            try
            {
                Reader = new StreamReader(WebReq.GetResponse().GetResponseStream());
                ResponseData = Reader.ReadToEnd();
            }
            catch (Exception ex)
            {
                WebReq.GetResponse().GetResponseStream().Close();
                Reader.Close();
                Reader = null;
                WebReq = null;
            }
            finally
            {

                WebReq.GetResponse().GetResponseStream().Close();
                Reader.Close();
                Reader = null;
                WebReq = null;
            }

            return ResponseData;
        }

    }
}
