using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Newtonsoft.Json;
using PinEverything.Entites;
using PinEverything.Services;

namespace PinEverything.Web.ajaxpage
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["op"] != null)
            {
                string op = Request["op"].ToString();
                switch (op)
                {
                    case "LoadUserDetail":
                        LoadUserDetail();
                        break;
                    case "AddPublic":
                        AddPublic();
                        break;
                }
            }
        }

        /// <summary>
        /// 获取用户基本信息
        /// </summary>
        private void LoadUserDetail()
        {
            JavaScriptObject resultObj = new JavaScriptObject();
            if (Session["user"] != null)
            {
                UserInfo userInfo = new UserInfo();
                userInfo = Session["user"] as UserInfo;
                JavaScriptObject userObj = new JavaScriptObject();
                userObj.Add("UserId", userInfo.UserId);
                userObj.Add("Avatar", userInfo.Avatar);
                userObj.Add("UserName", userInfo.UserName);

                resultObj.Add("MSG", "Y");
                resultObj.Add("Users", userObj);
            }
            else
                resultObj.Add("MSG", "N");

            PageResponse(resultObj);
        }

        /// <summary>
        /// 发布信息
        /// </summary>
        private void AddPublic()
        {
            JavaScriptObject resultObj = new JavaScriptObject();
            if (Session["user"] != null)
            {
                string startPlace = Request["startPlace"];
                string destination = Request["destination"];
                string carType = Request["carType"];
                string carColor = Request["carColor"];
                string ownerPhone = Request["ownerPhone"];
                string note = Request["note"];
                int pubType = int.Parse(Request["pubType"].ToString());

                UserInfo userInfo = new UserInfo();
                userInfo = Session["user"] as UserInfo;

                PYTService PYTService = new PYTService();

                bool flag = PYTService.AddPublishInfo(Guid.NewGuid(), userInfo.ProjectId, userInfo.UserId, pubType, 0, "", note, carType, carColor, 4);

                if (flag)
                {
                    resultObj.Add("MSG", "Y");
                    //发布动态
                    APIService APIService=new Services.APIService();
                    string access_token = userInfo.MDToken;
                    string pMsg=startPlace;
                    string title = startPlace;
                    bool postFlag = APIService.postUpdate(access_token, pMsg, title);
                }
                else
                    resultObj.Add("MSG", "N");
            }
            else
                resultObj.Add("MSG", "N");

            PageResponse(resultObj);
        }

        private void PageResponse(JavaScriptObject resultObj)
        {
            Response.Clear();
            Response.AddHeader("Content-Type", "Application/json");
            string jsonStr = JavaScriptConvert.SerializeObject(resultObj);
            Response.Write(jsonStr);
            Response.End();
        }
    }
}