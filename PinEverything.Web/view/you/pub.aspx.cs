using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using PinEverything.Entites;

namespace PinEverything.Web.view.you
{
    public partial class pub : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] != null)
            {
                string localState = string.Empty;
                string pubType = Request["pubType"];
                if (!string.IsNullOrEmpty(pubType))
                    this.hidPubType.Value = pubType;

                UserInfo userDetail = new UserInfo();
                userDetail = Session["user"] as UserInfo;

                string dateStr = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                this.ownerTime.Value = dateStr;

                //if (Session["localState"] != null)
                //    localState = Session["localState"].ToString();
                //this.startPosition.Value = localState;
                this.ownerPhone.Value = userDetail.Phone;
            }
            else
            {
                Response.Redirect("/callback.aspx");
            }
        }
    }
}