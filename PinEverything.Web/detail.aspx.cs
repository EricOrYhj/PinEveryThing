using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using PinEverything.Entites;
using PinEverything.Services;

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
                PublishInfo publicInfo=new PublishInfo();

                if (!string.IsNullOrEmpty(publishId))
                {
                    publicInfo = PYTService.GetPublicInfo(Guid.Parse(publishId));
                    UserInfo  userInfo=new UserInfo();
                    userInfo=PYTService.GetUser(publicInfo.UserId);
                    if(userInfo!=null)
                    {
                        this.userAvatar.InnerHtml = "<img src='" + userInfo.Avatar + "' />";
                        this.userName.InnerText = userInfo.UserName;
                    }
                    this.hidPublishId.Value = publishId;
                    this.startPlace.InnerText = publicInfo.Lat;
                    this.destination.InnerText = publicInfo.Lng;
                    this.carType.InnerText = publicInfo.PubContent;
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