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
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] != null)
            {
                UserInfo userDetail = new UserInfo();
                userDetail = Session["user"] as UserInfo;

                this.avatar.InnerHtml = "<img src='"+userDetail.Avatar+"' />";
                this.userNanme.InnerText = userDetail.UserName;
            }
            else
            {
                Response.Redirect("/callback.aspx");
            }
        }
    }
}