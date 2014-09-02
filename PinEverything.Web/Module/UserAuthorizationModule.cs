using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PinEverything.Entites;
using MySpider;
using System.Text;

using PinEverything.Services;
using Newtonsoft.Json;

namespace PinEverything.Web.Module
{
    public class UserAuthorizationModule : IHttpModule
    {
        /// <summary>
        /// 当前URL绝对路径
        /// </summary>
        string currAbsolutePath = string.Empty;
        string currRowUrl = string.Empty;

        public void Dispose()
        {
            //throw new NotImplementedException();
        }

        public void Init(HttpApplication context)
        {
            context.AcquireRequestState += new EventHandler(context_AcquireRequestState);
        }

        void context_AcquireRequestState(object sender, EventArgs e)
        {
            try
            {
                // 获取应用程序
                HttpApplication application = (HttpApplication)sender;
                currAbsolutePath = application.Request.Url.AbsolutePath == null ? "" : application.Request.Url.AbsolutePath.ToLower();
                currRowUrl = application.Request.Url.ToString();

                //验证aspx、ashx页面
                if (!MySpider.IO.GetExtraName(currAbsolutePath).Equals("aspx") && !MySpider.IO.GetExtraName(currAbsolutePath).Equals("ashx"))
                    return;

                //1.1 判断是否是CallBack页面
                //1.2 登陆状态设置，保存用户信息
                //1.3 页面跳转处理

                //2.1 判断是否登陆
                //2.2 未登录构造登陆链接跳转

                if (currAbsolutePath.ToLower().Contains(MySpider.ConfigHelper.GetConfigString("CallbackKey")))
                {
                    //获取code
                    string code = application.Request["code"];
                    string access_token = string.Empty;
                    /*
                     * TODO:
                     * 获取token
                     * 获取用户信息
                     * 保存到数据库
                     * 保存Session
                     * 
                     */
                    if (!string.IsNullOrEmpty(code))
                        access_token = APIService.GetAccessToken(code);

                    JavaScriptObject userObj = new JavaScriptObject();
                    UserInfo userDetail = new UserInfo();
                    if (!string.IsNullOrEmpty(access_token))
                    {

                        userObj = APIService.getUserDetail(access_token);
                        if (userObj != null)
                        {
                            //用户信息
                            userDetail.UserId = Guid.Parse(userObj["id"].ToString());
                            userDetail.UserName = userObj["name"].ToString();
                            userDetail.Avatar = userObj["avatar"].ToString();
                            userDetail.Email = userObj["email"].ToString();
                            userDetail.Phone = userObj["mobile_phone"].ToString();
                            userDetail.MDToken = access_token;

                            //用户公司信息
                            JavaScriptObject projectObj = (JavaScriptObject)userObj["project"];
                            userDetail.ProjectId = Guid.Parse(projectObj["id"].ToString());

                            PYTService PYTService = new PYTService();

                            //是否已经有该用户
                            UserInfo userInfo = PYTService.GetUser(Guid.Parse(userObj["id"].ToString()));
                            if (userInfo != null)
                            {
                                //更新Token
                                bool updateFlag = PYTService.UpdateUserMDToken(Guid.Parse(userObj["id"].ToString()), access_token);
                            }
                            else
                            {
                                //插入用户
                                bool addFlag = PYTService.AddUser(userDetail.ProjectId, userDetail.UserId, userDetail.MDToken, userDetail.UserName, userDetail.Email, userDetail.Phone,
                                    userDetail.CurrLat, userDetail.CurrLng, userDetail.Avatar);
                            }

                            
                        }

                    }
                    if (userObj != null) 
                    {
                        //获取用户数据库信息
                        application.Session["user"] = userDetail;
                    }
                    application.Session["login"] = 1;

                    //获取Token
                    if (application.Session["currRowUrl"] != null)
                    {
                        string url = application.Session["currRowUrl"].ToString();
                        application.Session["currRowUrl"] = null;
                        application.Response.Redirect(url);
                    }
                    else
                    {
                        application.Response.Redirect(MySpider.ConfigHelper.GetConfigString("Index"));
                    }

                }
                else
                {
                    int login = Convert.ToInt32(application.Session["login"]);

                    if (login == 0)
                    {
                        GotoLogin(application);
                        return;
                    }

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void GotoLogin(HttpApplication application)
        {
            //未登录构造授权链接并跳转
            string reqUrl = string.Format("{0}/auth2/authorize.aspx?app_key={1}&redirect_uri={2}",
                    MySpider.ConfigHelper.GetConfigString("ApiAddress"),
                    MySpider.ConfigHelper.GetConfigString("AppKey"),
                    MySpider.ConfigHelper.GetConfigString("CallbackUrl")
                );

            //记录当前页面
            application.Session["currRowUrl"] = currRowUrl;

            application.Response.Redirect(reqUrl);
        }


    }
}