using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Net;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using TokenModels;
using Newtonsoft.Json;
using System.Text;


namespace PinEverything.Web
{
    public partial class Home : System.Web.UI.Page
    {
        /*
         * 线上
         * api.mingdao  appkey=5EA1FD6F97B5 app_secret=F6E99B92FDAF2B2AEF029A3C673E466  grant_type=authorization_code 应用名 PinYouThing
         * http://api.mingdao.com/auth2/authorize.aspx?app_key=5EA1FD6F97B5&redirect_uri=http://localhost:46898/Home.aspx
         * http://api.mingdao.com/auth2/access_token
         * 测试环境
         * mdserver  appkey=353649313BAB app_secret=719F9644F36BD9456AC53412A1E084 应用名eric测试
         * http://mdserver/MD.api.web/auth2/authorize.aspx?app_key=353649313BAB&redirect_uri=http://localhost:46898/Home.aspx
         * http://mdserver/MD.api.web/auth2/access_token
         * 
         * redirect_uri//回调地址  根据实际情况调整
         */

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Cookies["username"] != null)
            {
                string username = Request.Cookies["username"].Value;
                this.name.InnerText = username;
            }
            else
            {
                //处理授权
                string code = Request.QueryString["code"];
                string requestURL = string.Empty;
                if (string.IsNullOrEmpty(code))//获取code
                {
                    requestURL = "http://api.mingdao.com/auth2/authorize.aspx?app_key=5EA1FD6F97B5&redirect_uri=http://localhost:46898/Home.aspx";
                    Response.Redirect(requestURL);
                }
                else if (!string.IsNullOrEmpty(code))//获取token
                {
                    string Url = "http://api.mingdao.com/auth2/access_token";
                    string QueryStr = "app_key=5EA1FD6F97B5&app_secret=F6E99B92FDAF2B2AEF029A3C673E466&grant_type=authorization_code&code=" + code + "&redirect_uri=http://localhost:46898/Home.aspx";
                    requestURL = HttpGet(Url, QueryStr);

                    AuthModel Auths = null;
                    Auths = XmlToObject<AuthModel>(requestURL);
                    if (Auths != null)
                    {
                        string access_token = !string.IsNullOrEmpty(Auths.access_token) ? Auths.access_token : string.Empty;

                        if (!string.IsNullOrEmpty(access_token))
                        {
                            JavaScriptObject userDetailObj = getUserDetail(access_token);

                            if (userDetailObj != null)
                            {
                                //用户信息
                                string userID = userDetailObj["id"].ToString();
                                string name = userDetailObj["name"].ToString();
                                string avatar = userDetailObj["avatar"].ToString();
                                string email = userDetailObj["email"].ToString();
                                string mobile_phone = userDetailObj["mobile_phone"].ToString();

                                //用户公司信息
                                JavaScriptObject projectObj = (JavaScriptObject)userDetailObj["project"];
                                string pID = projectObj["id"].ToString();
                                string pName = projectObj["name"].ToString();


                                Response.Cookies["userName"].Value = name;
                                Response.Cookies["userName"].Expires = DateTime.Now.AddDays(1);
                            }
                        }

                    }
                }
            }
        }

        public string HttpGet(string Url, string QueryStr)
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

        public string HttpPost(string url, Dictionary<string, string> paraDic)
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

        /// <summary>
        /// Deserializes an object (type indicated by <typeparamref name="T"/>) from the specified <paramref name="xml"/>.
        /// </summary>
        /// <typeparam name="T">The type of the object.</typeparam>
        /// <param name="xml">The xml string.</param>
        /// <returns>The object deserialized.</returns>
        public static T XmlToObject<T>(string xml) where T : class
        {
            return XmlToObject(typeof(T), xml) as T;
        }

        /// <summary>
        /// Deserializes an object (type indicated by <paramref name="type"/>) from the specified <paramref name="xml"/>.
        /// </summary>
        /// <param name="type">The type of the object.</param>
        /// <param name="xml">The xml string.</param>
        /// <returns>The object deserialized.</returns>
        public static object XmlToObject(Type type, string xml)
        {
            object result = null;
            using (TextReader reader = new StringReader(xml))
            {
                XmlSerializer s = new XmlSerializer(type);
                result = s.Deserialize(reader);
            }

            return result;
        }

        /// <summary>
        /// 获取授权账号基本信息
        /// </summary>
        /// <param name="access_token"></param>
        /// <returns>用户对象</returns>
        public JavaScriptObject getUserDetail(string access_token)
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
        public bool postUpdate(string access_token, string pMsg, string title)
        {
            bool flag = false;
            if (!string.IsNullOrEmpty(access_token))
            {
                string Url = "https://api.mingdao.com/post/update";//接口地址
                string fxUrl = "http://114.215.150.137:81/home.aspx";//URL地址
                //参数
                Dictionary<string, string> paramsDic = new Dictionary<string, string>();
                paramsDic.Add("access_token", access_token);
                paramsDic.Add("format", "json");
                paramsDic.Add("p_type", "1");
                paramsDic.Add("p_msg", pMsg);
                paramsDic.Add("l_uri", fxUrl);
                paramsDic.Add("l_title", title);
                paramsDic.Add("s_type", "3");

                string requestURL = HttpPost(Url, paramsDic);

                JavaScriptObject resultObj = new JavaScriptObject();
                if (!string.IsNullOrEmpty(requestURL))
                    resultObj = (JavaScriptObject)JavaScriptConvert.DeserializeObject(requestURL);

                if (!string.IsNullOrEmpty(resultObj["post"].ToString()))
                    flag = true;
            }
            return flag;
        }

        /// <summary>
        /// 发送私信
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="u_id">接收人ID</param>
        /// <param name="msg">私信内容</param>
        /// <param name="is_send_mobilephone">是否发送短信</param>
        /// <returns></returns>
        public bool sendMsg(string access_token, string u_id, string msg, string is_send_mobilephone)
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
            }
            return flag;
        }

    }
}