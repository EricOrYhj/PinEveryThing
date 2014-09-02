using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using PinEverything.Entites;

namespace PinEverything.Web
{
    public partial class owner : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] != null)
            {
                string pubType = Request["pubType"];
                if (!string.IsNullOrEmpty(pubType))
                    this.hidPubType.Value = pubType;

                UserInfo userDetail = new UserInfo();
                userDetail = Session["user"] as UserInfo;
            }
            else
            {
                Response.Redirect("/callback.aspx");
            }
        }
    }
}