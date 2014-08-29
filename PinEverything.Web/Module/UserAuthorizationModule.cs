using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
                currRowUrl = application.Request.RawUrl;

                //验证aspx、ashx页面
                if (!MySpider.IO.GetExtraName(currAbsolutePath).Equals("aspx") && !MySpider.IO.GetExtraName(currAbsolutePath).Equals("ashx"))
                    return;

                //1.1 判断是否是CallBack页面
                //1.2 登陆状态设置，保存用户信息
                //1.3 页面跳转处理

                //2 判断是否登陆

                if (currAbsolutePath.ToLower().Contains("callback.aspx"))
                {
                    
                }
                else
                {

                }

                application.Response.Write("hello world.");

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}