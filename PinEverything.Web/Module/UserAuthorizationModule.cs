using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PinEverything.Entites;
using MySpider;
using System.Text;

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
            throw new NotImplementedException();
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

                if (currAbsolutePath.ToLower().Contains("callback.aspx"))
                {
                    //获取code
                    string code = application.Request["code"];

                    application.Session["login"] = 1;

                    /*
                     * TODO:
                     * 获取token
                     * 获取用户信息
                     * 保存到数据库
                     * 保存Session
                     * 
                     */


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

                //application.Response.Write("hello world.");


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