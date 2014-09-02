using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using PinEverything.Entites;
using PinEverything.Services;
using Newtonsoft.Json;

namespace PinEverything.Web
{
    public partial class detail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] != null)
            {
                string publishId = Request["publishId"];
                PYTService PYTService = new PYTService();
                PublishInfo publicInfo = new PublishInfo();

                if (!string.IsNullOrEmpty(publishId))
                {
                    UserInfo curUserInfor = new UserInfo();
                    curUserInfor = Session["user"] as UserInfo;

                    

                    Guid curUserID = curUserInfor.UserId;

                    int type = 1;//可以加入
                    publicInfo = PYTService.GetPublicInfo(Guid.Parse(publishId));
                    UserInfo userInfo = new UserInfo();
                    userInfo = PYTService.GetUser(publicInfo.UserId);
                    btnTel.HRef = "tel://" + userInfo.Phone;
                    if (curUserID.Equals(publicInfo.UserId))
                        type = 2;//是创建人

                    if (userInfo != null)
                    {
                        this.userAvatar.InnerHtml = "<img src='" + userInfo.Avatar + "' />";
                        this.userName.InnerText = userInfo.UserName;
                    }
                    this.hidPublishId.Value = publishId;
                    this.startPlace.InnerText = publicInfo.StartPosition;
                    this.destination.InnerText = publicInfo.EndPosition;
                    this.carType.InnerText = publicInfo.CarType;
                    this.num.InnerText = "限" + publicInfo.UserLimCount+"人";
                    this.ownerTime.InnerText = publicInfo.StarTime.ToString("yyyy/MM/dd HH:mm:ss");

                    //加入的成员信息
                    Entites.EntityList<JoinInfo> joinMemberList = new EntityList<JoinInfo>();
                    joinMemberList = PYTService.QueryJoinMembers(Guid.Parse(publishId));
                    List<JoinInfo> joinMemberInfo = new List<JoinInfo>();
                    joinMemberInfo = joinMemberList.Table;

                    JavaScriptArray joinMemberArr = new JavaScriptArray();

                    foreach (JoinInfo joinItem in joinMemberInfo)
                    {
                        Guid userID = joinItem.UserId;
                        if (userID.Equals(curUserID))
                            type = 3;//已加入
                    }

                    string html = string.Empty;
                    switch (type){
                        case 1 :
                            html = "<a href=\"javascript:Detail.JoinPublic();\" id=\"join\" style=\"display: block;\">加入</a>";
                            break;
                        case 2:
                            html = "<a href=\"javascript:Detail.CanclePublic();\" id=\"exit\" style=\"display: block;\">取消发布</a>";
                            break;
                        case 3:
                            html = "<a href=\"javascript:Detail.ExitJoin();\" id=\"cancle\" style=\"display: block;\">退出</a>";
                            break;
                    }
                    this.sidebarPlay.InnerHtml = html;
                }
                else
                    Response.Redirect("/callback.aspx");
            }
            else
            {
                Response.Redirect("/callback.aspx");
            }
        }
    }
}