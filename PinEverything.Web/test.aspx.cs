using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PinEverything.Services;

namespace PinEverything.Web
{
    public partial class test : System.Web.UI.Page
    {
        PYTService pServ = new PYTService();

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = "测试页";
        }

        protected void btnLoadPub_Click(object sender, EventArgs e)
        {
            this.LoadData();
        }

        protected void btnAddPub_Click(object sender, EventArgs e)
        {
            this.pServ.AddPublishInfo(
                    Guid.NewGuid(),
                    Guid.NewGuid(),
                    Guid.NewGuid(),
                    1,
                    1,
                    "测试标题" +MySpider.Rand.Str(6),
                    "测试内容" + MySpider.Rand.Str_char(20),
                    "106.123133",
                    "23.3123123",
                    3,
                    "",
                    "",
                    "",
                    ""

                );
            this.LoadData();
        }

        private void LoadData()
        {
            this.grvData.DataSource = this.pServ.QueryPublishInfo().Table;
            this.grvData.DataBind();
        }
    }
}